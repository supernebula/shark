
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IScheduler
    {
        Task PushAsync(RequestMessage message);
        Task PushAsync(IEnumerable<RequestMessage> message);

        RequestMessage Poll();

    }
}
