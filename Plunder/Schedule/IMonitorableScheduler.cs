
namespace Plunder.Schedule
{
    public interface IMonitorableScheduler : IScheduler
    {
        int CurrentQueueCount();

        int AccumulatedTotal();
    }
}
