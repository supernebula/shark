using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder;
using Plunder.Download;
using Plunder.Process.Analyze;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Compoment;
using Plunder.Plugin.Download;
using Plunder.Plugin.Filter;
using Plunder.Plugin.Pipeline;
using Plunder.Schedule;

namespace PlunderTestConsole
{
    public class SchedulerTest
    {
        private static SchedulerTest _instance;
        public static SchedulerTest Instance => _instance ?? (_instance = new SchedulerTest());

        private Crawler _crawler;
        public void RunCrawler()
        {
            //var memoryBloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            //_crawler = new Crawler(new SequenceScheduler(memoryBloomFilter));
            var redisBloomFilter = new RedisBloomFilter<string>(1000 * 10, 1000 * 10 * 20, "localhost", 6379);
            _crawler = new Crawler(new SequenceScheduler(redisBloomFilter));
            //var downloaders = new List<IDownloader> { new FakeDownloader(4) };
            var downloaders = new List<IDownloader> { new HttpClientDownloader(4) };
            _crawler.RegisterDownloader(downloaders);
            _crawler.RegisterPageAnalyzer<UsashopcnPageAnalyzer>(UsashopcnPageAnalyzer.SiteId);
            _crawler.RegisterResultPipeModule(new ConsoleModule(500, 0, 400, 500, true, true));

            _crawler.Start(WebPageType.Static, SiteIndex.Usashopcn, GetUrls());

            var statusTimer = new Timer(crawler =>
            {
                var status = ((Crawler) crawler).RunStatusInfo();
                Console.WriteLine(String.Format("QueueCount:{0}, TaskCount={1}, ConsumeTotal:{2},  ResultTotal:{3}", status.QueueCount, status.TaskCount, status.ConsumeTotal, status.ResultTotal));
            }, _crawler, 2000, 2000);

        }


        string[] GetUrls()
        {
            var urls = new List<string>();
            for (int i = 0; i < 1; i++)
            {
                urls.Add("http://www.usashopcn.com/Product/Details/120039?num=" + i);
                //urls.Add("https://detail.tmall.com/item.htm?spm=a223v.7914393.2320796782.3.Eih3aS&id=529389366427&abbucket=_AB-M972_B17&acm=03683.1003.1.670563&aldid=vlaSgHMR&abtest=_AB-LR972-PR972&scm=1003.1.03683.ITEM_529389366427_670563&pos=3&nummmmmm=" + i);
            
            }

            return urls.ToArray();
        }
    }
}
