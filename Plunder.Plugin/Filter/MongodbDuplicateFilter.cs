using Plunder.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Filter
{
    public class MongodbDuplicateFilter : IDuplicateFilter<string>
    {
        public MongodbDuplicateFilter(object dbcontext)
        {

        }

        void IDuplicateFilter<string>.Add(string item)
        {
            throw new NotImplementedException();
        }

        bool IDuplicateFilter<string>.Contains(string item)
        {
            throw new NotImplementedException();
        }
    }
}
