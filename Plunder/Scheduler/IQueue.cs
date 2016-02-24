using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IQueue<T>
    {
        void Add(IMessage message);

        T Toke();

        void Clear();
    }
}
