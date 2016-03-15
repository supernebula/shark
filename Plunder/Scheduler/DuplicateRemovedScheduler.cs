using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plunder.Storage;

namespace Plunder.Scheduler
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    {

        private readonly BlockingCollection<RequestMessage> _queue;

        protected int AccumulatedMessageTotal { get; set; }

        protected DuplicateRemovedScheduler()
        {
            _queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            AccumulatedMessageTotal = 0;
        }



        public RequestMessage Poll()
        {
            AccumulatedMessageTotal++;
            var message = _queue.Take();
            return message;
        }

        public bool Push(RequestMessage message)
        {
            return _queue.TryAdd(message);
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
