using System;

namespace PlunderTestConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(@"开始");
            SchedulerTest.Instance.RunCrawler();

             

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
