using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Test.Pipeline
{
    public class ConsoleModule : IPipelineModule
    {
        public Task<int> ProcessAsync(PageResult data, int serialNumber)
        {
            Trace.WriteLine(serialNumber + ".1");
            return Save(data, serialNumber);
        }


        private async Task<int> Save(PageResult data, int serialNumber)
        {
            Trace.WriteLine(serialNumber + ".2");
            var result = await (new HttpClient() { Timeout = new TimeSpan(0, 0, 20) }).GetStringAsync("http://www.gnc.com/Vitamins/Fish-Oil-Omegas/category.jsp?categoryId=12961314");
            Trace.WriteLine(serialNumber + ".3, length:" + result.Length + ",gnc-html:" + result.Substring(0, 5000).Trim());
            return result.Length;
        }
    }
}
