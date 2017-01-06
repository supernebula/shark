using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Plunder.Filter;
using Plunder.Util;
using System.Linq;

namespace Plunder.Scheduler
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    { 

        protected readonly BlockingCollection<RequestMessage> Queue;

        private readonly IBloomFilter<string> _bloomFilter;

        private int _accumulatedMessageTotal;

        protected int AccumulatedMessageTotal => _accumulatedMessageTotal;

        protected DuplicateRemovedScheduler(IBloomFilter<string> bloomFilter)
        {
            _bloomFilter = bloomFilter;
            Queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            _accumulatedMessageTotal = 0;
        }

        public RequestMessage WaitUntillPoll()
        {
            //AccumulatedMessageTotal++;
            Interlocked.Increment(ref _accumulatedMessageTotal);
            
            return Queue.Take();
        }

        public RequestMessage Poll()
        {
            //AccumulatedMessageTotal++;
            Interlocked.Increment(ref _accumulatedMessageTotal);
            RequestMessage message;
            Queue.TryTake(out message, 0);
            return message;
        }

        public List<RequestMessage> Poll(int size)
        {
            var result = new List<RequestMessage>();
            while (size > 0)
            {
                RequestMessage message;
                if (Queue.TryTake(out message, 0))
                {
                    result.Add(message);
                    //AccumulatedMessageTotal++;
                    Interlocked.Increment(ref _accumulatedMessageTotal);
                }
                size--;
            }
            return result;
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
            if (_bloomFilter.Contains(message.Request.Url))
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"重复:{message.Request.Url}");
                Console.ForegroundColor = originalColor;
                return true;
            }
                
            _bloomFilter.Add(message.Request.Url);
            return false;
        }

        public async Task<bool> PushAsync(RequestMessage message)
        {
            return await Task.Run(() => Queue.TryAdd(message));
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
    }
}
