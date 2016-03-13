using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder;
using Plunder.Scheduler;
using Plunder.Downloader;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Pipeline;
using Plunder.Plugin.Downloader;

namespace PlunderConsole
{
    public class Program
    {

        static void Main(string[] args)
        {
            ConsoleMultiAreaTest();
        }


        static void ConsoleMultiAreaTest()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(i + "AAAAA AAAAA AAAAA AAAAA" + i);
            }

            
            for (int i = 0; i < 100; i++)
            {
                if (i > 19)
                    Console.SetCursorPosition(50, 19);
                else
                    Console.SetCursorPosition(50, i);

                Thread.Sleep(300);
                Console.WriteLine(i + "BBBBB BBBBB BBBBB BBBBB" + i);
                if (i > 19)
                {
                    Console.SetCursorPosition(50, 19);
                    Console.MoveBufferArea(50, 1, 25, 20, 50, 0);
                }
                    
            }

            Console.ReadKey();
        }


        static void RunSpider()
        {
            Spider _spider;
            _spider = new Spider(new LineScheduler());

            _spider.RegisterPageAnalyzer<UsashopcnPageAnalyzer>("usashopcn");
            _spider.RegisterDownloader("simpleDownload", (topic) => new HttpClientDownloaderBak());
            _spider.RegisterDownloader("dynamicDownload", (topic) => new PhantomJSDownloaderBak());
            _spider.RegisterPipeModule(new ConsoleModule(500, 0, 400, 500, true, true));

            _spider.Start();

            var statusTimer = new Timer(spider => { Console.WriteLine(((Spider) spider).RunStatusInfo()); }, _spider, 0, 2000);
        }

    }
}   