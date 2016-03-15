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

        protected BlockingCollection<RequestMessage> Queue { get; set; }

        protected int AccumulatedMessageTotal { get; set; }

        protected DuplicateRemovedScheduler()
        {
            Queue = new BlockingCollection<RequestMessage>(new ConcurrentQueue<RequestMessage>());
            AccumulatedMessageTotal = 0;
        }



        public RequestMessage Poll()
        {
            AccumulatedMessageTotal++;
            var message = Queue.Take();
            return message;
        }

        public bool Push(RequestMessage message)
        {
            return Queue.TryAdd(message);
        }

        public async Task<bool> PushAsync(RequestMessage message)
        {
            return await Task.Run(() => Queue.TryAdd(message));
        }

        public void Push(IEnumerable<RequestMessage> messages)
        {
            foreach (var message in messages)
            {
                Queue.TryAdd(message);
            }
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
