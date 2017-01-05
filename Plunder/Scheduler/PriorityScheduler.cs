using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Filter;

namespace Plunder.Scheduler
{
    public class PriorityScheduler : DuplicateRemovedScheduler
    {
        public PriorityScheduler(IBloomFilter<string> bloomFilter) : base(bloomFilter)
        {
        }
    }
}
