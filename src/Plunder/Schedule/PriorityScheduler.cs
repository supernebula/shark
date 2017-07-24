using Plunder.Schedule.Filter;

namespace Plunder.Schedule
{
    public class PriorityScheduler : DuplicateRemovedScheduler
    {
        public PriorityScheduler(IBloomFilter<string> bloomFilter, EngineContext engineContext) : base(bloomFilter, engineContext)
        {
        }
    }
}
