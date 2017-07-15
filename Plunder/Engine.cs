using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Plunder.Process.Analyze;
using Plunder.Compoment;
using Plunder.Schedule;
using Plunder.Download;
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




        #region Required Unit




        private ConsumerBroker _consumerBroker;
        
        private IRequestMessageProvider _requestMessageProvider;

        #endregion

        #region Initialization

        public Engine(EngineOptions options)
        {
            _options = options;
        }

        ////public Engine(IMonitorableScheduler scheduler)
        ////{
        ////    Scheduler = scheduler;
        ////    _resultPipeline = new ResultItemPipeline();
        ////    _resultPipeline.RegisterModule(new DefaultMomeryProducerModule(Scheduler)); //ProducerModule is required
        ////    _downloaders = new List<IDownloader>();
        ////    _pageAnalyzerTypes = new Dictionary<string, Type>();
        ////    _seedRequests = new List<RequestMessage>();
        ////}

        private bool CheckConfig(out string error)
        {
            Console.WriteLine("检查配置");
            var checkInfo = new StringBuilder();
            if (Scheduler == null)
                checkInfo.AppendLine("Error:缺少具体的Scheduler");
            if (ResultPipeline == null)
                checkInfo.AppendLine("Error:缺少ResultPipeline");
            else
            {
                if (ResultPipeline.ModuleCount == 0)
                    checkInfo.AppendLine("Error:ResultPipeline没有包含任何Module");
                if (!ResultPipeline.IsContainProducer())
                    checkInfo.AppendLine("Error:ResultPipeline缺少ProducerModule");
            }

            ////if (_downloaders == null || !_downloaders.Any())
            ////    checkInfo.AppendLine("Error:缺少Downloader");

            ////if (_pageAnalyzerTypes == null || !_pageAnalyzerTypes.Any())
            ////    checkInfo.AppendLine("Error:缺少PageAnalyzer");

            error = checkInfo.ToString();
            return checkInfo.Length == 0;
        }

        #endregion

        #region addition
        public void RegisterResultPipeModule(params IResultPipelineModule[] modules)
        {
            ResultPipeline.RegisterModule(modules);
        }

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

        public void RegisterMessageProvider(IRequestMessageProvider requestMessageProvider)
        {
            _requestMessageProvider = requestMessageProvider;
            throw new NotImplementedException();
        }

        #endregion

        #region Running and Monitoring

        public EngineMonitor RunStatusInfo()
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

        #endregion

        public void Start(IEnumerable<RequestMessage> seedRequests)
        {
            SeekRequests.AddRange(seedRequests);
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
            SeekRequests.AddRange(seeds);
            Run();
        }

        private void Run()
        {
            string err;
            if (!CheckConfig(out err))
            {
                Console.WriteLine(err);
                return;
            } 
            

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
    }
}
