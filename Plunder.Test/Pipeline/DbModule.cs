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
    public class DbModule : IPipelineModule
    {
        public Task<int> ProcessAsync(PageResult data, int serialNumber)
        {
            Trace.WriteLine(serialNumber + ".1");
            return Save(data, serialNumber);
        }


        private async Task<int> Save(PageResult data, int serialNumber)
        {
            Trace.WriteLine(serialNumber + ".2");
            var result = await (new HttpClient() { Timeout = new TimeSpan(0, 0, 20) }).GetStringAsync("http://www.finishline.com/");
            Trace.WriteLine(serialNumber + ".3, length:" + result.Length + ",finishline-html:" + result.Substring(0, 20));
            return result.Length;
        }
    }
}
