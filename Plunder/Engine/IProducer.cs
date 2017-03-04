using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Schedule;

namespace Plunder.Engine
{
    public interface IProducer
    {
        IMessage<IEnumerable<Request>> Deliver();
    }
}
