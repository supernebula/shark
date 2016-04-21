using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder;
using Plunder.Downloader;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Compoment;
using Plunder.Plugin.Downloader;
using Plunder.Plugin.Pipeline;
using Plunder.Scheduler;

namespace PlunderTestConsole
{
    public class SchedulerTest
    {
        private static SchedulerTest _instance;
        public static SchedulerTest Instance => _instance ?? (_instance = new SchedulerTest());

        private Spider _spider;
        public void RunSpider()
        {
            _spider = new Spider(new SequenceScheduler());
            var downloaders = new List<IDownloader> { new FakeDownloader(4) };
            _spider.RegisterDownloader(downloaders);
            _spider.RegisterPageAnalyzer<UsashopcnPageAnalyzer>(UsashopcnPageAnalyzer.SiteId);
            _spider.RegisterPipeModule(new ConsoleModule(500, 0, 400, 500, true, true));

            _spider.Start(TopicType.StaticHtml, SiteIndex.Usashopcn, GetUrls());

            var statusTimer = new Timer(spider =>
            {
                var status = ((Spider) spider).RunStatusInfo();
                Console.WriteLine(String.Format("QueueCount:{0}, TaskCount={1}, ConsumeTotal:{2},  ResultTotal:{3}", status.QueueCount, status.TaskCount, status.ConsumeTotal, status.ResultTotal));
            }, _spider, 2000, 2000);

        }


        string[] GetUrls()
        {
            var urls = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                urls.Add("http://www.usashopcn.com/Product/Details/120039?num=" + i);
            }

            return urls.ToArray();
        }
    }
}
