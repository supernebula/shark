using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class LineScheduler : DuplicateRemovedScheduler
    {
        public override List<RequestMessage> Poll(int size)
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
    }
}
