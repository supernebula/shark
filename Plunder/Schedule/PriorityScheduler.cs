using Plunder.Schedule.Filter;

namespace Plunder.Schedule
{
    public class PriorityScheduler : DuplicateRemovedScheduler
    {
        public PriorityScheduler(IBloomFilter<string> bloomFilter) : base(bloomFilter)
        {
        }
    }
}
