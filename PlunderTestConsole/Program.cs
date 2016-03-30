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
            EventSingnThreadTest();
            Console.ReadKey();
        }

        static void PipelineTest()
        {
            Trace.WriteLine("#start statment");
            var pipeline = new ExecutePipeline();
            pipeline.Inject(new PageResult());
            Trace.WriteLine("#end statment");

            
        }


        static void EventSingnThreadTest()
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false); //声明 false， WaitOne时线程等待
            var thread = new Thread(() =>
            {
                Console.WriteLine(@"1.ConsumerBroker Start");
                Console.WriteLine(@"2.Excute Consume");
                Task.Run(async () =>
                {
                    Console.WriteLine(@"3.Task doing...");
                    await Task.Delay(10000);
                    Console.WriteLine(@"4.Task end");
                    autoResetEvent.Set();//终止信号，允许等待线程继续。。
                });
                Console.WriteLine(@"5.End of the Task declarations. Start WaitOne");
                autoResetEvent.WaitOne();//线程等待
                Console.WriteLine(@"6.ConsumerBroker End");
            });
            thread.Start();
        }
    }
}
