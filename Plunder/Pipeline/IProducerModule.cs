using Plunder.Schedule;
using System.Threading.Tasks;

namespace Plunder.Pipeline
{
    public interface IProducerModule
    {
        IScheduler Scheduler { get; }

        Task ScheduleAsync(params RequestMessage[] reqMsgs);
    }
}

