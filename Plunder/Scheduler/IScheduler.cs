
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IScheduler
    {
        Task PushAsync(IMessage message);
        Task PushAsync(IEnumerable<IMessage> message);

        IMessage Poll();

    }
}
