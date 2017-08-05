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

        private Engine _engine;
        public void LaunchEngine()
        {
            ////var memoryBloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            ////_engine = new Engine(new SequenceScheduler(memoryBloomFilter));
            //var redisBloomFilter = new RedisBloomFilter<string>(1000 * 10, 1000 * 10 * 20, "localhost", 6379);
            //_engine = new Engine(new SequenceScheduler(redisBloomFilter));
            //////var downloaders = new List<IDownloader> { new FakeDownloader(4) };
            //var downloaders = new List<IDownloader> { new HttpClientDownloader(4) };
            //_engine.RegisterDownloader(downloaders);
            //_engine.RegisterPageAnalyzer<DevTestPageAnalyzer>(DevTestPageAnalyzer.SiteId);
            //_engine.RegisterResultPipeModule(new MultiAreaConsoleModule(500, 0, 400, 500, true, true));

            //_engine.Start(WebPageType.Static, SiteIndex.DevTest, GetUrls());

            //var statusTimer = new Timer(engine =>
            //{
            //    var status = ((Engine) engine).RunStatusInfo();
            //    Console.WriteLine(String.Format("QueueCount:{0}, TaskCount={1}, ConsumeTotal:{2},  ResultTotal:{3}", status.QueueCount, status.TaskCount, status.ConsumeTotal, status.ResultTotal));
            //}, _engine, 2000, 2000);

        }


        string[] GetUrls()
        {
            var urls = new List<string>();
            for (int i = 0; i < 1; i++)
            {
                urls.Add("http://www.devtest.com/artice/item/120039?num=" + i);
            
            }

            return urls.ToArray();
        }
    }
}
