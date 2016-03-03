
namespace Plunder.Scheduler
{
    public interface IScheduler
    {
        void Push(IMessage message);

        IMessage Poll();

    }
}
