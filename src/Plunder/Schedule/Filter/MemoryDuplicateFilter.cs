using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Schedule.Filter;
using Evol.Common;

namespace Plunder.Schedule.Filter
{
    /// <summary>
    ///内存重复过滤器
    /// </summary>
    class MemoryDuplicateFilter : IDuplicateFilter<string>
    {
        public void Add(string item)
        {
            MemoryStorage<SimpleItem, string>.Instance.Add(new SimpleItem() { Id = item, Value = item });
        }

        public bool Contains(string item)
        {
            var flag = MemoryStorage<SimpleItem, string>.Instance.Contains(item);
            return flag;
        }
    }

    public class SimpleItem : IPrimaryKey<string>
    {
        public string Id { get; set; }

        public string Value { get; set;}
    }
}
