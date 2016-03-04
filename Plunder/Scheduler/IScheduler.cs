
using System.Collections.Generic;

namespace Plunder.Scheduler
{
    public interface IScheduler
    {
        void Push(IMessage message);
        void Push(IEnumerable<IMessage> message);

        IMessage Poll();

    }
}
