using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IScheduler
    {
        void Push<T>(IMessage<T> message);

        IMessage<T> Poll<T>();
    }
}
