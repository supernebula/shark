
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plunder.Schedule
{
    public interface IScheduler : IDisposable
    {
        Task<bool> PushAsync(RequestMessage message);
        Task PushAsync(IEnumerable<RequestMessage> message);

        void Push(IEnumerable<RequestMessage> message);

        RequestMessage WaitUntillPoll();

        RequestMessage Poll();

        List<RequestMessage> Poll(int size);

    }
}
