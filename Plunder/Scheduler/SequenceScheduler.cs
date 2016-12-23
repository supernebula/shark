using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Filter;

namespace Plunder.Scheduler
{
    public class SequenceScheduler : DuplicateRemovedScheduler
    {
        public SequenceScheduler(IBloomFilter<string> bloomFilter) : base(bloomFilter)
        {
        }
    }
}
