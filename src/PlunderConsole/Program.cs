using Plunder;
using Plunder.Compoment;
using Plunder.Download;
using Plunder.Pipeline;
using Plunder.Plugin.Analyze;
using Plunder.Plugin.Download;
using Plunder.Plugin.Pipeline;
using Plunder.Schedule;
using Plunder.Schedule.Filter;
using System;
using System.Collections.Generic;
using Autofac;
using Plunder.Storage.MongoDB;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;
using Plunder.Storage.MongoDB.Repositories;
using Plunder.Plugin.Filter;
using Plunder.Configuration;

namespace PlunderConsole
{

    public class Program
    {



        static Engine _engine;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            InitAppConfig();
            LaunchEngine();
        }

        static void InitAppConfig()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<AccessRecordRepository>().As<AccessRecordRepository>();
            containerBuilder.RegisterType<ProductRepository>().As<ProductRepository>();
            containerBuilder.RegisterType<PlantRepository>().As<PlantRepository>();
            containerBuilder.RegisterType<PlantPhotoRepository>().As<PlantPhotoRepository>();

            containerBuilder.RegisterType<PlunderMongoDBContext>().As<PlunderMongoDBContext>();
            containerBuilder.RegisterType<MongoDbContextProvider>().As<IMongoDbContextProvider>();
            
            var container = containerBuilder.Build();
            var iocManager = new DefaultIocManager(container);
            AppConfig.Init(iocManager);
        }

        static void LaunchEngine()
        {
            var options = BuildOptions();
            _engine = new Engine(options);
            //_engine.Start(new List<RequestMessage> { new RequestMessage() {  } }); //添加起始链接
            _engine.Start("www.plant.csdb.cn", "http://www.plant.csdb.cn/names?page=0"); //添加起始链接
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
            //var bloomFilter = new MemoryBloomFilter<string>(1000 * 10, 1000 * 10 * 20);
            var bloomFilter = new RedisBloomFilter<string>(1000 * 10, 1000 * 10 * 20, "127.0.0.1", 6379, true);
            var scheduler = new SequenceScheduler(bloomFilter);
            return scheduler;
        }

        static DownloaderFactory InitDownloaderFactory()
        {
            var downloaderThunks = new Dictionary<PageType, Func<Request, PageType, IDownloader>>();
            downloaderThunks.Add(PageType.Static, (req, pageType) => new HttpClientDownloader(req, pageType));
            var factory = new DownloaderFactory(downloaderThunks);
            return factory;
        }

        static PageAnalyzerFactory InitPageAnalyzerFactory()
        {
            var factory = new PageAnalyzerFactory();
            factory.Register(DevTestPageAnalyzer.SiteIdValue, DevTestPageAnalyzer.TargetPageFlagValue, () => new DevTestPageAnalyzer());
            factory.Register(PlantCsdbListPageAnalyzer.SiteIdValue, PlantCsdbListPageAnalyzer.TargetPageFlagValue, () => new PlantCsdbListPageAnalyzer());
            factory.Register(PlantCsdbPhotoPageAnalyzer.SiteIdValue, PlantCsdbPhotoPageAnalyzer.TargetPageFlagValue, () => new PlantCsdbPhotoPageAnalyzer());
            return factory;
        }

        static ResultItemPipeline InitResultItemPipeline()
        {
            var resultPipeline = new ResultItemPipeline();
           // resultPipeline.RegisterModule(new ConsoleResultModule());
            resultPipeline.RegisterModule(new PlantCollectPipelineModule());
            
            return resultPipeline;
        }
    }
}
