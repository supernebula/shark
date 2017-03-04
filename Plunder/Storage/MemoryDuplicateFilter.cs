using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Schedule.Filter;

namespace Plunder.Storage
{
    class MemoryDuplicateFilter : IDuplicateFilter<string>
    {
        public void Add(string item)
        {
            MemoryStorage<string, string>.Instance.Add();
        }

        public bool Contains(string item)
        {
            throw new NotImplementedException();
        }
    }
}
