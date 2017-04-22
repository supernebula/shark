using System;
using System.Collections.Generic;
using System.Threading;
using Plunder;
using Plunder.Schedule;
using Plunder.Download;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Compoment;
using Plunder.Plugin.Pipeline;
using Plunder.Plugin.Download;
using Plunder.Plugin.Filter;
using Plunder.Schedule.Filter;

namespace PlunderConsole
{
    public class Program
    {
        static Crawler _crawler;
        static void Main(string[] args)
        {
            RunCrawler();
        }
        static void RunCrawler()
        {
            var bloomFilter  = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            //var bloomFilter = new RedisBloomFilter<string>(1000 * 10, 1000 * 10 * 20, "127.0.0.1", 6379, true);
            _crawler = new Crawler(new SequenceScheduler(bloomFilter));
            var downloaders = new List<IDownloader> { new HttpClientDownloader(4) };
            _crawler.RegisterDownloader(downloaders);
            _crawler.RegisterPageAnalyzer<UsashopcnPageAnalyzer>(UsashopcnPageAnalyzer.SiteId);
            _crawler.RegisterResultPipeModule(new ConsoleModule());

            _crawler.Start(WebPageType.Static, SiteIndex.Usashopcn, "http://www.usashopcn.com/");

            var statusTimer = new Timer(crawler => { Console.WriteLine(((Crawler) crawler).RunStatusInfo()); }, _crawler, 0, 2000);
        }


        //#region 控制台多缓冲区测试

        //static void ConsoleMultiAreaTest()
        //{
        //    Console.SetCursorPosition(0, 0);
        //    for (int i = 0; i < 20; i++)
        //    {
        //        Console.WriteLine(i + "AAAAA AAAAA AAAAA AAAAA" + i);
        //    }


        //    for (int i = 0; i < 100; i++)
        //    {
        //        if (i > 19)
        //            Console.SetCursorPosition(50, 19);
        //        else
        //            Console.SetCursorPosition(50, i);

        //        Thread.Sleep(300);
        //        Console.WriteLine(i + "BBBBB BBBBB BBBBB BBBBB" + i);
        //        if (i > 19)
        //        {
        //            Console.SetCursorPosition(50, 19);
        //            Console.MoveBufferArea(50, 1, 25, 20, 50, 0);
        //        }

        //    }

        //    Console.ReadKey();
        //}

        //#endregion

    }
}   