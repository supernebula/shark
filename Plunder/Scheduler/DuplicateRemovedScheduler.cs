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

        public Task PushAsync(RequestMessage message)
        {
            return Task.Run(() =>
            {
                if(!MessageRecord.IsExist(message.HashCode))
                    MessageRecord.Add(message.HashCode, message.Request.Uri);
            });
            
        }

        public Task PushAsync(IEnumerable<RequestMessage> messages)
        {
            return Task.Run(() =>
            {
                messages.ToList().ForEach(e =>
                {
                    if (!MessageRecord.IsExist(e.HashCode))
                        MessageRecord.Add(e.HashCode, e.Request.Uri);
                });
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
    }
}
