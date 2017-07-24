using Plunder;
using Plunder.Download;
using Plunder.Pipeline;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Download;
using Plunder.Plugin.Pipeline;
using Plunder.Schedule;
using Plunder.Schedule.Filter;
using System;
using System.Collections.Generic;

namespace PlunderConsole
{

    public class Program
    {
        static Engine _engine;
        static void Main(string[] args)
        {
            LaunchEngine();
        }

        static void LaunchEngine()
        {
            var options = BuildOptions();
            _engine = new Engine(options);
            _engine.Start(new List<RequestMessage> { new RequestMessage() }); //添加起始链接
        }

        static EngineOptions BuildOptions()
        {
            var options = new EngineOptions()
            {
                Scheduler = InitScheduler(),
                DownloaderFactory = InitDownloaderFactory(),
                PageAnalyzerFactory = InitPageAnalyzerFactory(),
                ResultPipeline = InitResultItemPipeline()
            };

            return options;
        }

        static IMonitorableScheduler InitScheduler()
        {
            var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            //var bloomFilter = new RedisBloomFilter<string>(1000 * 10, 1000 * 10 * 20, "127.0.0.1", 6379, true);
            var scheduler = new SequenceScheduler(bloomFilter, default(EngineContext));
            return scheduler;
        }

        static DownloaderFactory InitDownloaderFactory()
        {
            var downloaderThunks = new Dictionary<PageType, Func<IDownloaderOld>>();
            downloaderThunks.Add(PageType.Static, () => new HttpClientDownloaderOld(4));
            var factory = new DownloaderFactory(downloaderThunks);
            return factory;
        }

        static PageAnalyzerFactory InitPageAnalyzerFactory()
        {
            var factory = new PageAnalyzerFactory();
            factory.Register(UsashopcnPageAnalyzer.SiteIdValue, UsashopcnPageAnalyzer.PageTagValue, () => new UsashopcnPageAnalyzer());
            return factory;
        }

        static ResultItemPipeline InitResultItemPipeline()
        {
            var resultPipeline = new ResultItemPipeline();
            resultPipeline.RegisterModule(new ConsoleResultModule());
            return resultPipeline;
        }
    }
}
