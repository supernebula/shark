using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Test.Pipeline;
using Plunder.Compoment;

namespace PlunderTestConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Trace.WriteLine("#start statment");
            var pipeline = new ExecutePipeline();
            pipeline.Inject(new PageResult());
            Trace.WriteLine("#end statment");

            Console.ReadKey();

        }
    }
}
