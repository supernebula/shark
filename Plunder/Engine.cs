using System;
using System.Collections.Generic;
using System.Linq;
using Plunder.Compoment;
using Plunder.Schedule;
using Plunder.Pipeline;

namespace Plunder
{

    public class Engine
    {
        private readonly EngineOptions _options;

        public IMonitorableScheduler Scheduler { get; private set; }

        public DownloaderFactory DownloaderFactory { get; private set; } //private readonly List<IDownloader> _downloaders;

        public PageAnalyzerFactory PageAnalyzerFactory { get; private set; } //private readonly Dictionary<string, Type> _pageAnalyzerTypes;

        public ResultItemPipeline ResultPipeline { get; private set; } //private readonly ResultItemPipeline _resultPipeline;

        public List<RequestMessage> SeekRequests { get; private set; } //private List<RequestMessage> _seedRequests;

        public Engine(EngineOptions options)
        {
            _options = options;
        }

        public void Start(IEnumerable<RequestMessage> seedRequests)
        {
            if (seedRequests == null || !seedRequests.Any())
                throw new ArgumentNullException(nameof(seedRequests));
            SeekRequests.AddRange(seedRequests);
            List<string> errors;
            if (!CheckOptions(_options, out errors))
            {
                Console.WriteLine(string.Join("\r\n", errors));
            }
            Run();
        }

        public void Start(string topic, string siteId, params string[] urls)
        {
            var seeds = new List<RequestMessage>();
            foreach (var url in urls)
            {
                var seed = new RequestMessage()
                {
                    Topic = topic,
                    Request = new Request() { SiteId = siteId, Url = url }
                };
                seeds.Add(seed);
            }
            Start(seeds);
        }

        private void Run()
        {
#if DEBUG

            //_consumerBroker = new ConsumerBroker(5, Scheduler, _downloaders, ResultPipeline, _pageAnalyzerTypes);
            Scheduler.Push(SeekRequests);
            _consumerBroker.Start();
#else
            var thread = new Thread(() =>
            {
                _consumerBroker = new ConsumerBroker(10, _scheduler, _downloaders, _resultPipeline, _pageAnalyzerTypes);
                _scheduler.PushAsync(_seedRequests);
            _consumerBroker.Start();
            });
            thread.Start();
#endif
        }

        private bool CheckOptions(EngineOptions options, out List<string> errors)
        {
            errors = new List<string>();
            if (options.Scheduler == null)
                errors.Add("Error:缺少Scheduler");

            if (!options.DownloaderFactory.Any())
                errors.Add("Error:DownloaderFactory缺少Downloader");

            if (!options.PageAnalyzerFactory.Any())
                errors.Add("Error:PageAnalyzerFactory缺少PageAnalyzerFactory");

            if (options.ResultPipeline == null)
                errors.Add("Error:缺少ResultPipeline");
            else
            {
                if (options.ResultPipeline.ModuleCount == 0)
                    errors.Add($"Error:ResultPipeline没有包含任何{nameof(IResultPipelineModule)}");
                if (!options.ResultPipeline.IsContainProducer())
                    errors.Add($"Error:ResultPipeline缺少{nameof(IProducerModule)}");
            }

            return errors.Count == 0;
        }


        public EngineMonitor GetRunStatusInfo()
        {
            var status = new EngineMonitor()
            {
                QueueCount = Scheduler.CurrentQueueCount(),
                TaskCount = _consumerBroker.DownloadingTaskCount(),
                ConsumeTotal = _consumerBroker.ConsumeTotal,
                ResultTotal = ResultPipeline.ResultTotal
            };
            return status;
        }



        #region Required Unit




        private ConsumerBroker _consumerBroker;
        
        private IRequestMessageProvider _requestMessageProvider;




        ////public Engine(IMonitorableScheduler scheduler)
        ////{
        ////    Scheduler = scheduler;
        ////    _resultPipeline = new ResultItemPipeline();
        ////    _resultPipeline.RegisterModule(new DefaultMomeryProducerModule(Scheduler)); //ProducerModule is required
        ////    _downloaders = new List<IDownloader>();
        ////    _pageAnalyzerTypes = new Dictionary<string, Type>();
        ////    _seedRequests = new List<RequestMessage>();
        ////}



        #endregion

        #region addition
        //public void RegisterResultPipeModule(params IResultPipelineModule[] modules)
        //{
        //    ResultPipeline.RegisterModule(modules);
        //}

        ////public void RegisterPageAnalyzer<T>(string siteId) where T : IPageAnalyzer, new()
        ////{
        ////    _pageAnalyzerTypes.Add(siteId, typeof(T));
        ////}

        ////public void RegisterDownloader(IDownloader downloader)
        ////{
        ////    _downloaders.Add(downloader);
        ////}

        ////public void RegisterDownloader(IEnumerable<IDownloader> downloaders)
        ////{
        ////    _downloaders.AddRange(downloaders);
        ////}

        //public void RegisterMessageProvider(IRequestMessageProvider requestMessageProvider)
        //{
        //    _requestMessageProvider = requestMessageProvider;
        //    throw new NotImplementedException();
        //}

        #endregion

        #region Running and Monitoring

        #endregion






    }
}
