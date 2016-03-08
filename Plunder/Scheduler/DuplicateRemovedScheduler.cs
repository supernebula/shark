using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public abstract class DuplicateRemovedScheduler : IMonitorableScheduler
    {
        //protected abstract void AddToHistory(IMessage message);

        //protected abstract void IsExitInHistory(IMessage message);

        protected BlockingCollection<IMessage> Queue { get; set; }

        protected int AccumulatedMessageTotal { get; set; }

        protected DuplicateRemovedScheduler()
        {
            Queue = new BlockingCollection<IMessage>(new ConcurrentQueue<IMessage>());
            AccumulatedMessageTotal = 0;
        }



        public IMessage Poll()
        {
            AccumulatedMessageTotal++;
            var message = Queue.Take();
            //AddToHistory(message);
            return message;
            throw new NotImplementedException();
        }

        public Task PushAsync(IMessage message)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(IEnumerable<IMessage> message)
        {
            throw new NotImplementedException();
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
