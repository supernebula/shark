//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Owin.Hosting;

//namespace Plunder.WebHost
//{
//    public class ProgramApp
//    {
//        static void Main(string[] args)
//        {
//            int workThreads; int completionPortThreads;
//            ThreadPool.GetMaxThreads(out workThreads, out completionPortThreads);
//            Trace.WriteLine(String.Format("MaxWorkThreads:{0}, MaxCompletionPortThreads:{1}", workThreads, completionPortThreads ));
//            int workThreads2; int completionPortThreads2;
//            ThreadPool.GetMinThreads(out workThreads2, out completionPortThreads2);
//            Trace.WriteLine(String.Format("MinWorkThreads:{0}, MinCompletionPortThreads:{1}", workThreads2, completionPortThreads2));

//            //using (WebApp.Start<ConsoleStartup>("http://localhost:5000"))
//            //{
//            //    Console.WriteLine("listen in http://localhost:5000");
//            //    Console.WriteLine("Press [Enter] to quit...");
//            //    Console.ReadLine();
//            //}
//        }
//    }
//}
