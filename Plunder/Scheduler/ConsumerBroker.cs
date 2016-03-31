using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Plunder.Analyze;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Pipeline;


namespace Plunder.Scheduler
{
    public class ConsumerBroker : IDisposable
    {
        private readonly IMonitorableScheduler _scheduler;
        private readonly List<IDownloader> _downloaders;
        private readonly ConcurrentDictionary<string, Type> _pageAnalyzerTypes;
        private readonly ResultPipeline _resultPipeline;
        private readonly int _maxDownloadThreadNumber;
        private AutoResetEvent _messagePullAutoResetEvent;

        private bool _pulling;

        public ConsumerBroker(int maxDownloadThreadNumber, IMonitorableScheduler scheduler, IEnumerable<IDownloader> downloaders, ResultPipeline resultPipeline, IEnumerable<KeyValuePair<string, Type>> pageAnalyzerTypes)
        {
            _maxDownloadThreadNumber = maxDownloadThreadNumber;
            _scheduler = scheduler;
            _downloaders = new List<IDownloader>();
            _downloaders.AddRange(downloaders);
            _downloaders.GroupBy(e => e.Topic).ToList().ForEach(g =>
            {
                if (g.Count() > 1)
                    throw new ArgumentException("downloader.Topic不能重复", "downloaders");
            });

            _resultPipeline = resultPipeline;
            _pageAnalyzerTypes = new ConcurrentDictionary<string, Type>();
            pageAnalyzerTypes.ToList().ForEach(t => _pageAnalyzerTypes.TryAdd(t.Key, t.Value));
            _messagePullAutoResetEvent = new AutoResetEvent(false);


        }

        private IPageAnalyzer GeneratePageAnalyzer(Site site)
        {
            Type analyzerType;
            _pageAnalyzerTypes.TryGetValue(site.Domain, out analyzerType);
            if (analyzerType == null || analyzerType.GetInterface(typeof (IPageAnalyzer).Name) == null)
                return null;
            return (IPageAnalyzer)TypeDescriptor.CreateInstance(null, analyzerType, null, null);
        }

        private int DownloadingTaskCount()
        {
            return _downloaders.Sum(e => e.DownloadingTaskCount);
        }

        public void Start()
        {
            PullMessage();
        }

        private bool _stopPull = false;

        private void PullMessage()
        {
            while (true)
            {
                if (_stopPull)
                    break;
                _messagePullAutoResetEvent.WaitOne();
                if (_pulling || DownloadingTaskCount() >= _maxDownloadThreadNumber)
                {
                    _messagePullAutoResetEvent.Reset();
                    continue;
                }

                _pulling = true;
                var message = _scheduler.Poll();
                _pulling = false;

                if (message == null)
                {
                    _messagePullAutoResetEvent.Reset();
                    continue;
                }
                Consume(message);
                _messagePullAutoResetEvent.Reset();
            }
        }


        private void Consume(params RequestMessage[] messages)
        {
            _downloaders.ForEach(downloader =>
            {
                var reqs = messages.Where(e => e.Topic.Equals(downloader.Topic)).Select(m => m.Request);
                downloader.DownloadAsync(reqs, (resp) =>
                {
                    _messagePullAutoResetEvent.Set();
                    var pageAnalyzer = GeneratePageAnalyzer(resp.Request.Site);
                    var pageResult = pageAnalyzer.Analyze(resp);
                    _resultPipeline.Inject(pageResult);
                });
            });
        }

        public void Dispose()
        {
            _messagePullAutoResetEvent.Close();
            _messagePullAutoResetEvent.Dispose();
            _messagePullAutoResetEvent = null;
            _stopPull = true;
            Thread.EndThreadAffinity();
        }
    }
}
