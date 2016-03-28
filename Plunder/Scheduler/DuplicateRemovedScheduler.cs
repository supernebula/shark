using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Utilities;

namespace Plunder.Scheduler
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    {

        private readonly BlockingCollection<RequestMessage> _queue;

        private readonly BloomFilter<string> _bloomFilter; 

        protected int AccumulatedMessageTotal { get; set; }

        protected DuplicateRemovedScheduler()
        {
            _bloomFilter  = new BloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            _queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            AccumulatedMessageTotal = 0;
        }

        public RequestMessage Poll()
        {
            AccumulatedMessageTotal++;
            RequestMessage message;
            _queue.TryTake(out message, 0);
            return message;
        }

        public List<RequestMessage> Poll(int size)
        {
            var result = new List<RequestMessage>();
            while (size > 0)
            {
                RequestMessage message;
                if (_queue.TryTake(out message, 0))
                {
                    result.Add(message);
                    AccumulatedMessageTotal++;
                }
                size--;
            }
            return result;
        }

        public bool Push(RequestMessage message)
        {
            if (_bloomFilter.Contains(message.Request.Uri))
                return false;
            if (!_queue.TryAdd(message))
                return false;
            _bloomFilter.Add(message.Request.Uri);
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
