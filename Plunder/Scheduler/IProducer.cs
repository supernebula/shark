using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Scheduler
{
    public interface IProducer
    {
        IMessage<IEnumerable<Request>> Deliver();
    }
}
