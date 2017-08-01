using NLog;
using Plunder.Compoment;
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
        private ILogger Logger = LogManager.GetLogger("trigger");
        private SchedulerContext _context;
        private readonly ConcurrentDictionary<string, TriggerTaskItem> _downloadTaskCollection = new ConcurrentDictionary<string, TriggerTaskItem>();
        //private int _downloadingTaskCount = 0;
        private readonly int _maxDownloadThreadNumber;
        private AutoResetEvent _messagePullAutoResetEvent;
        private bool _pulling = false;

        public Trigger(SchedulerContext schedulerContext, int maxDownLoadThreadNumber)
        {
            _context = schedulerContext ?? throw new ArgumentNullException(nameof(schedulerContext));
            _maxDownloadThreadNumber = maxDownLoadThreadNumber;
            _messagePullAutoResetEvent = new AutoResetEvent(false);
        }

        public int DownloadingTaskCount()
        {
            return _downloadTaskCollection.Count();
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
                    if(_messagePullAutoResetEvent.WaitOne())
                        _first = false;
                }

                var downloadingTaskCount = DownloadingTaskCount();
                Logger.Debug($"Count:downing={downloadingTaskCount}, max={_maxDownloadThreadNumber}");
                if (_pulling || downloadingTaskCount >= _maxDownloadThreadNumber)
                {
                    var threadDown = _messagePullAutoResetEvent.Reset();
                    _messagePullAutoResetEvent.WaitOne();
                    //Logger.Debug($"threadDown:{threadDown}=eventLock.Reset()");
                    continue;
                }

                Thread.Sleep(300);

                var num = _maxDownloadThreadNumber - DownloadingTaskCount();
                var messages = _context.Scheduler.Poll(num);

                if (messages == null || !messages.Any())
                {
                    var message = _context.Scheduler.WaitUntillPoll();
                    messages.Add(message);
                }
                _messagePullAutoResetEvent.Reset();
                //Logger.Debug(message.Request.Url);

                foreach (var item in messages)
                {
                    var cancelTokenSource = new CancellationTokenSource();
                    var token = cancelTokenSource.Token;
                    var downloadTask = Task.Factory.StartNew(async (state) => {
                        var taskState = state as DownloadTaskState;
                        if (taskState == null)
                            cancelTokenSource.Cancel();
                        token.ThrowIfCancellationRequested();
                        await WorkingAsync(taskState.Request, token);

                    },
                        new DownloadTaskState() { Context = _context, Request = item.Request },
                        token
                    );

                    downloadTask.ContinueWith(t => {
                        Downloaded(item.Request.Id);
                    });

                    var taskItem = new TriggerTaskItem()
                    {
                        Id = item.Request.Id,
                        CancelTokenSource = cancelTokenSource,
                        DownloadTask = downloadTask
                    };

                    _downloadTaskCollection.TryAdd(taskItem.Id, taskItem);
                }
            }
        }

        private async Task WorkingAsync(Request request, CancellationToken token)
        {
            if (request == null)
                return;
            var delay = request.DelaySecond * 1000;
            if(delay > 0)
                Thread.Sleep(delay);
            var downloader = _context.DownloaderFactory.Create(request, request.PageType);
            request.Downloader = downloader.GetType().Name;
            await downloader.DownloadAsync(token)
                .ContinueWith(t => {
                    if (t.IsCanceled)
                    {
                        Logger.Debug("Download Task Canceled:" + t.Exception?.Message);
                        return;
                    }
                        
#if DEBUG
                    Logger.Debug("Downloaded:" + t.Result.Request.Url);
#endif

                        var pageAnalyzer = _context.PageAnalyzerFactory.Create(t.Result.Request.SiteId, t.Result.Request.Channel);
                    var pageResult = pageAnalyzer.Analyze(t.Result);
                    _context.ResultPipeline.Inject(pageResult);
                });
        }

        private void Downloaded(string downTaskId)
        {
            //ConsumeTotal++;
            TriggerTaskItem taskItem = null;
            _downloadTaskCollection.TryRemove(downTaskId, out taskItem);

            //_downloadTaskCount--
            //Interlocked.Add(ref _downloadingTaskCount, -1);
            _messagePullAutoResetEvent.Set();
            //Logger.Debug($"eventLock.Set()");
        }
    }
}
