using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{

    public interface ICustomerPool<T> : ICustomerPool
    {
        T Take();
    }

    public interface ICustomerPool
    {
    }
}
