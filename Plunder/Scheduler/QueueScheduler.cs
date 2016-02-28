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
        BlockingCollection<IMessage> _queue;

        public QueueScheduler()
        {
            _queue = new BlockingCollection<IMessage>(new ConcurrentQueue<IMessage>());
        }

        public int MessageCount()
        {
            return _queue.Count();
        }

        public IMessage Poll()
        {
            return _queue.Take();
        }

        public void Push(IMessage message)
        {
            _queue.Add(message); 
        }
    }
}
