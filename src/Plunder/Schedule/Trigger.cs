using Plunder.Download;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Plunder.Schedule
{
    public class Trigger
    {
        private SchedulerContext _context;
        private readonly List<IDownloaderOld> _downloaders;
        
        private readonly ConcurrentDictionary<string, IDownloader> _downloaderCollection;
        private readonly int _maxDownloadThreadNumber;
        private AutoResetEvent _messagePullAutoResetEvent;
        private bool _pulling;

        public Trigger(SchedulerContext schedulerContext, int maxDownLoadThreadNumber)
        {
            if (schedulerContext == null)
                throw new ArgumentNullException(nameof(schedulerContext));
            _context = schedulerContext;
            _maxDownloadThreadNumber = maxDownLoadThreadNumber;
            _messagePullAutoResetEvent = new AutoResetEvent(false);
        }

        public int DownloadingTaskCount()
        {
            return _downloaders.Count();
        }

        public void Start()
        {
            PullMessage();
        }

        public void Stop()
        {
            _stopPull = true;
        }
        private bool _stopPull;

        private bool _first = true;

        private void PullMessage()
        {
            while (true)
            {
                if (_stopPull)
                    break;
                if (!_first)
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
                var message = _context.Scheduler.Poll();
                _pulling = false;

                if (message == null)
                    message = _context.Scheduler.WaitUntillPoll();
                _messagePullAutoResetEvent.Reset();
                Working(message);
            }
        }


        private void Working(params RequestMessage[] messages)
        {
            //var downloader = ne

            _downloaders.ForEach(downloader =>
            {
                var reqs = messages.Where(e => e.Topic.Equals(downloader.PageType)).Select(m => m.Request).ToList();
                reqs.ForEach(request =>
                {
                    downloader.DownloadAsync(request)
                    .ContinueWith(t => {
                        Downloaded();
                        return t.Result;
                    })
                    .ContinueWith(t =>
                    {

                        //Console.WriteLine("Downloaded Html:" + t.Result.Item2.Content);
#if DEBUG
                        Console.WriteLine("Downloaded:" + t.Result.Item1.Url);
#endif
                        var pageAnalyzer = _context.PageAnalyzerFactory.Create(t.Result.Item1.SiteId, t.Result.Item1.Channel);
                        var pageResult = pageAnalyzer.Analyze(t.Result.Item1, t.Result.Item2);
                        _context.ResultPipeline.Inject(pageResult);
                    });
                });
            });
        }

        private void Downloaded()
        {
            //ConsumeTotal++;
            _messagePullAutoResetEvent.Set();
        }
    }
}
