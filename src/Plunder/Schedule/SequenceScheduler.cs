using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Schedule.Filter;

namespace Plunder.Schedule
{
    public class SequenceScheduler : DuplicateRemovedScheduler
    {
        public SequenceScheduler(IDuplicateFilter<string> duplicateFilter) : base(duplicateFilter)
        {
        }
    }
}
