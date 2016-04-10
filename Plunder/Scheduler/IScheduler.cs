
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IScheduler : IDisposable
    {
        Task<bool> PushAsync(RequestMessage message);
        Task PushAsync(IEnumerable<RequestMessage> message);

        RequestMessage WaitUntillPoll();

        RequestMessage Poll();

        List<RequestMessage> Poll(int size);

    }
}
