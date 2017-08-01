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
        private int _downloadingTaskCount = 0;
        private readonly int _maxDownloadThreadNumber;
        private AutoResetEvent _messagePullAutoResetEvent;
        private bool _pulling = false;

        public Trigger(SchedulerContext schedulerContext, int maxDownLoadThreadNumber)
        {
            _context = schedulerContext ?? throw new ArgumentNullException(nameof(schedulerContext));
            _maxDownloadThreadNumber = maxDownLoadThreadNumber;
            _messagePullAutoResetEvent = new AutoResetEvent(false);
        }

        //public int DownloadingTaskCount()
        //{
        //    return _downloadingTaskCount;
        //}

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

                //var downloadingNumber = DownloadingTaskCount();
                Logger.Debug($"downingCount:{_downloadingTaskCount}");
                if (_pulling || _downloadingTaskCount >= _maxDownloadThreadNumber)
                {
                    if(_messagePullAutoResetEvent.Reset())
                        continue;
                }

                

                Thread.Sleep(300);
;
                //_pulling = true;
                var message = _context.Scheduler.Poll();
                //_pulling = false;

                if (message == null)
                    message = _context.Scheduler.WaitUntillPoll();
                _messagePullAutoResetEvent.Reset();

                var cancelTokenSource = new CancellationTokenSource();
                var token = cancelTokenSource.Token;
                var downloadTask = Task.Factory.StartNew(async (state) => {
                        var taskState = state as DownloadTaskState;
                        if (taskState == null)
                            cancelTokenSource.Cancel();
                        token.ThrowIfCancellationRequested();
                    await WorkingAsync(taskState.Request, token);

                }, 
                    new DownloadTaskState() { Context = _context, Request = message.Request },
                    token
                );

                downloadTask.ContinueWith(t => {
                    Downloaded(message.Request.Id);
                });

                var taskItem = new TriggerTaskItem()
                {
                    Id = message.Request.Id,
                    CancelTokenSource = cancelTokenSource,
                    DownloadTask = downloadTask
                };

                _downloadTaskCollection.TryAdd(taskItem.Id, taskItem);
                //_downloadTaskCount++;
                Interlocked.Add(ref _downloadingTaskCount, 1);
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
            await downloader.DownloadAsync(token)
                .ContinueWith(t => {

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
            Interlocked.Add(ref _downloadingTaskCount, -1);
            _messagePullAutoResetEvent.Set();
        }
    }
}
