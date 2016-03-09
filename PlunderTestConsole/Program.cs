using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Test.Pipeline;

namespace PlunderTestConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Trace.WriteLine("#start statment");
            var pipeline = new ExecutePipeline();
            pipeline.Inject(new DataResult());
            Trace.WriteLine("#end statment");

            Console.ReadKey();

        }
    }
}
