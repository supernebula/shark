using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder.Test.Pipeline;
using Plunder.Compoment;

namespace PlunderTestConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(@"开始");
            SchedulerTest.Instance.RunSpider();

            

            Console.ReadKey();
        }

        //static void PipelineTest()
        //{
        //    Trace.WriteLine("#start statment");
        //    var pipeline = new ExecutePipeline();
        //    pipeline.Inject(new PageResult());
        //    Trace.WriteLine("#end statment");
        //}


 
    }
}
