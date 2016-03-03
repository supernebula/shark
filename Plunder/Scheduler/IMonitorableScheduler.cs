
namespace Plunder.Scheduler
{
    public interface IMonitorableScheduler : IScheduler
    {
        int CurrentQueueCount();

        int AccumulatedTotal();
    }
}
