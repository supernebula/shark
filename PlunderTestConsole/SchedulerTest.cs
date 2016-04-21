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

            _spider.Start(TopicType.StaticHtml, SiteIndex.Usashopcn, "http://www.usashopcn.com/Product/Details/120039");
        }
    }
}
