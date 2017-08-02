using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Plunder.Schedule.Filter;
using NLog;

namespace Plunder.Schedule
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    {
        private ILogger Logger = LogManager.GetLogger("scheduler");

        private SchedulerContext _currentContext;

        private Trigger _trigger;

        protected readonly BlockingCollection<RequestMessage> Queue;


        private readonly IDuplicateFilter<string> _duplicateFilter;

        private int _accumulatedMessageTotal;

        protected int AccumulatedMessageTotal => _accumulatedMessageTotal;

        protected DuplicateRemovedScheduler(IDuplicateFilter<string> duplicateFilter)
        {
            _duplicateFilter = duplicateFilter;
            Queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            _accumulatedMessageTotal = 0;
        }


        public void RegisterContext(EngineContext engineContext)
        {
            _currentContext = new SchedulerContext(engineContext.Scheduler,
            engineContext.DownloaderFactory,
            engineContext.ResultPipeline,
            engineContext.PageAnalyzerFactory);
            _trigger = new Trigger(_currentContext, 1000);
        }

        public RequestMessage WaitUntillPoll()
        {
            //AccumulatedMessageTotal++;
            Interlocked.Exchange(ref _accumulatedMessageTotal, _accumulatedMessageTotal + 1);
            return Queue.Take();
        }

        private AutoResetEvent pollEventLock = new AutoResetEvent(true);
        public RequestMessage Poll()
        {
            //AccumulatedMessageTotal++;
            Interlocked.Add(ref _accumulatedMessageTotal, 1);
            RequestMessage message;
            Queue.TryTake(out message, 0);
            return message;
        }

        private object pollSizeLock = new object();

        public List<RequestMessage> Poll(int size)
        {

            lock (pollSizeLock)
            {
                var result = new List<RequestMessage>();
                while (size > 0)
                {
                    RequestMessage message;
                    if (Queue.TryTake(out message, 1))
                    {
                        result.Add(message);
                        //AccumulatedMessageTotal++;
                        Interlocked.Add(ref _accumulatedMessageTotal, 1);
                    }
                    else
                        return result;
                    size--;
                }
                return result;
            }

        }

        public bool Push(RequestMessage message)
        {
            if (IsDuplicate(message))
                return false;
            if (!Queue.TryAdd(message))
                return false;
            return true;
        }

        public bool IsDuplicate(RequestMessage message)
        {
            if (_duplicateFilter.Contains(message.Request.Url))
            {
                //Logger.Info($"Duplicated!");
                return true;
            }
                
            _duplicateFilter.Add(message.Request.Url);
            return false;
        }

        public async Task<bool> PushAsync(RequestMessage message)
        {
            return await Task.Run(() =>
            {
                if (IsDuplicate(message))
                    return false;
                return Queue.TryAdd(message);
            });
        }

        public void Push(IEnumerable<RequestMessage> messages)
        {
            messages.ToList().ForEach(e => Push(e));
            Console.WriteLine("ConcurrentQueue.Count:" + Queue.Count);
        }

        public async Task PushAsync(IEnumerable<RequestMessage> messages)
        {
            await Task.Run(() =>
            {
                foreach (var message in messages)
                {
                    if(!IsDuplicate(message))
                        Queue.TryAdd(message);
                }
            });
        }


        #region Monitor

        public int CurrentQueueCount()
        {
            return Queue.Count;
        }

        public int AccumulatedTotal()
        {
            return AccumulatedMessageTotal;
        }



        #endregion

        public void Dispose()
        {
            Queue.Dispose();
        }

        public void Start()
        {
            _trigger.Start();
        }

        public void Stop()
        {
            _trigger.Stop();
        }
    }
}
