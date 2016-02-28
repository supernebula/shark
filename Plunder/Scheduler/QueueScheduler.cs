using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class QueueScheduler : IScheduler
    {
        BlockingCollection<IMessage> _messageQueue;

        public QueueScheduler()
        {
            _messageQueue = new BlockingCollection<IMessage>(new ConcurrentQueue<IMessage>());
        }

        public IMessage Poll<T>()
        {
            return _messageQueue.Take();
            throw new NotImplementedException();
        }

        public void Push<T>(IMessage<T> message)
        {
            throw new NotImplementedException();
        }
    }
}
