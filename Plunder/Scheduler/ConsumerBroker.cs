using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Plunder.Analyze;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Pipeline;


namespace Plunder.Scheduler
{
    public class ConsumerBroker
    {
        private readonly IMonitorableScheduler _scheduler;
        private readonly Dictionary<string, IDownloader> _downloaders;
        private readonly ConcurrentDictionary<string, Type> _pageAnalyzerTypes;
        private readonly ResultPipeline _resultPipeline;
        private readonly int _maxDownloadThreadNumber;

        private readonly Timer _messagePullTimer;

        private bool _pulling;

        public ConsumerBroker(int maxDownloadThreadNumber, IMonitorableScheduler scheduler, IEnumerable<IDownloader> downloaders, ResultPipeline resultPipeline, IEnumerable<KeyValuePair<string, Type>> pageAnalyzerTypes)
        {
            _maxDownloadThreadNumber = maxDownloadThreadNumber;
            _scheduler = scheduler;
            _downloaders = new Dictionary<string, IDownloader>();
            downloaders.ToList().ForEach(d => _downloaders.Add(d.Topic, d));
            _resultPipeline = resultPipeline;
            _pageAnalyzerTypes = new ConcurrentDictionary<string, Type>();
            pageAnalyzerTypes.ToList().ForEach(t => _pageAnalyzerTypes.TryAdd(t.Key, t.Value));
            _messagePullTimer = new Timer((state) => PullMessage(), null, 0, 2000);
           
        }

        private IPageAnalyzer GeneratePageAnalyzer(Site site)
        {
            Type analyzerType;
            _pageAnalyzerTypes.TryGetValue(site.Domain, out analyzerType);
            if (analyzerType == null || analyzerType.GetInterface(typeof (IPageAnalyzer).Name) == null)
                return null;
            return (IPageAnalyzer)TypeDescriptor.CreateInstance(null, analyzerType, null, null);
        }

        private int CurrentDownloadThreadCount()
        {
            return _downloaders.ToList().Sum(e => e.Value.ThreadCount());
        }

        public void StartConsume()
        {
            PullMessage();
        }

        private void PullMessage()
        {
            if (CurrentDownloadThreadCount() >= _maxDownloadThreadNumber) return;
            if (_pulling)  return;
            _pulling = true;
            var message = _scheduler.Poll();
            _pulling = false;

            if(message == null) return;
            var task1 = Task.Run(() => Consume(message, () => { }));
        }


        private void PullMessage(int size)
        {
            if (_pulling) return;
            _pulling = true;
            var messages = _scheduler.Poll(size);
            _pulling = false;

            var tasks = new List<Task>();
            messages.ForEach((m) =>
            {
                tasks.Add(Task.Run(() => { Consume(m, PullMessage); }));
            });

            Task.WhenAll(tasks);
        }


        private async void Consume(RequestMessage message, Action callback)
        {
            IDownloader downloader;
            _downloaders.TryGetValue(message.Topic, out downloader);
            if (downloader == null)
                return;
            var response = await downloader.DownloadAsync(message.Request);
            var pageAnalyzer = GeneratePageAnalyzer(message.Request.Site);
            var pageResult = pageAnalyzer.Analyze(response);
            _resultPipeline.Inject(pageResult);
            callback();
        }
    }
}
