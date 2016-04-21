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
        public int ConsumeTotal { get; private set; }

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
                    throw new ArgumentException("downloader.Topic不能重复", nameof(downloaders));
            });

            _resultPipeline = resultPipeline;
            _pageAnalyzerTypes = new ConcurrentDictionary<string, Type>();
            pageAnalyzerTypes.ToList().ForEach(t => _pageAnalyzerTypes.TryAdd(t.Key, t.Value));
            _messagePullAutoResetEvent = new AutoResetEvent(false);
        }

        private IPageAnalyzer GeneratePageAnalyzer(string siteId)
        {
            var site = new Site();//todo: 根据siteId获取具体的Site
            Type analyzerType;
            _pageAnalyzerTypes.TryGetValue(site.Domain, out analyzerType);
            if (analyzerType == null || analyzerType.GetInterface(typeof (IPageAnalyzer).Name) == null)
                return null;
            return (IPageAnalyzer)TypeDescriptor.CreateInstance(null, analyzerType, null, null);
        }

        public int DownloadingTaskCount()
        {
            return _downloaders.Sum(e => e.DownloadingTaskCount);
        }

        public void Start()
        {
            PullMessage();
        }

        private bool _stopPull;

        private bool _first = true;

        private void PullMessage()
        {
            while (true)
            {
                if (_stopPull)
                    break;
                if(!_first)
                {
                    _messagePullAutoResetEvent.WaitOne();
                    _first = false;
                }

                var downloadingNumber = DownloadingTaskCount();

                if (_pulling || downloadingNumber >= _maxDownloadThreadNumber)
                {
                    _messagePullAutoResetEvent.Reset();
                    continue;
                }

                _pulling = true;
                var message = _scheduler.Poll();
                _pulling = false;

                if (message == null)
                    message = _scheduler.WaitUntillPoll();
                _messagePullAutoResetEvent.Reset();
                Consume(message);
                
            }
        }

        private void Consume(params RequestMessage[] messages)
        {
            _downloaders.ForEach(downloader =>
            {
                var reqs = messages.Where(e => e.Topic.Equals(downloader.Topic)).Select(m => m.Request).ToList();
                downloader.DownloadAsync(reqs, (req, resp) =>
                {

                    //ConsumeTotal++; //并发问题
                    _messagePullAutoResetEvent.Set();
                    Console.WriteLine("_messagePullAutoResetEvent.Set()");
                    return;

                    var pageAnalyzer = GeneratePageAnalyzer(req.SiteId);
                    var pageResult = pageAnalyzer.Analyze(req, resp);
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
