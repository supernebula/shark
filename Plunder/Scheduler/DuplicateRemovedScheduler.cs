using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Utilities;

namespace Plunder.Scheduler
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    {

        protected readonly BlockingCollection<RequestMessage> _queue;

        private readonly BloomFilter<string> _bloomFilter; 

        protected int AccumulatedMessageTotal { get; set; }

        protected DuplicateRemovedScheduler()
        {
            _bloomFilter  = new BloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            _queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            AccumulatedMessageTotal = 0;
        }

        public RequestMessage WaitUntillPoll()
        {
            AccumulatedMessageTotal++;
            return _queue.Take();
        }

        public RequestMessage Poll()
        {
            AccumulatedMessageTotal++;
            RequestMessage message;
            _queue.TryTake(out message, 0);
            return message;
        }

        public abstract List<RequestMessage> Poll(int size);

        public bool Push(RequestMessage message)
        {
            if (_bloomFilter.Contains(message.Request.Url))
                return false;
            if (!_queue.TryAdd(message))
                return false;
            _bloomFilter.Add(message.Request.Url);
            return true;
        }

        public async Task<bool> PushAsync(RequestMessage message)
        {
            return await Task.Run(() => _queue.TryAdd(message));
        }

        public void Push(IEnumerable<RequestMessage> messages)
        {
            foreach (var message in messages)
            {
                _queue.TryAdd(message);
            }
        }

        public async Task PushAsync(IEnumerable<RequestMessage> messages)
        {
            await Task.Run(() =>
            {
                foreach (var message in messages)
                {
                    _queue.TryAdd(message);
                }
            });
        }


        #region Monitor

        public int CurrentQueueCount()
        {
            return _queue.Count;
        }

        public int AccumulatedTotal()
        {
            return AccumulatedMessageTotal;
        }



        #endregion

        public void Dispose()
        {
            _queue.Dispose();
        }
    }
}
