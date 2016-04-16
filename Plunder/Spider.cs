using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Plunder.Analyze;
using Plunder.Compoment;
using Plunder.Scheduler;
using Plunder.Downloader;
using Plunder.Pipeline;

namespace Plunder
{

    public class Spider
    {
        #region Required Unit

        private readonly IMonitorableScheduler _scheduler;
        private readonly ResultPipeline _resultPipeline;
        private readonly List<IDownloader> _downloaders;
        private readonly Dictionary<string, Type> _pageAnalyzerTypes;
        private ConsumerBroker _consumerBroker;
        private List<RequestMessage> _seedRequests;
        private IRequestMessageProvider _requestMessageProvider;

        #endregion

        #region Initialization

        public Spider(IMonitorableScheduler scheduler)
        {
            _scheduler = scheduler;
            _resultPipeline = new ResultPipeline();
            _resultPipeline.RegisterModule(new ProducerModule(_scheduler)); //ProducerModule is required
            _downloaders = new List<IDownloader>();
            _pageAnalyzerTypes = new Dictionary<string, Type>();
            _seedRequests = new List<RequestMessage>();
        }

        private bool CheckConfig()
        {
            //todo:加长各项配置
            throw new NotImplementedException();
        }

        #endregion

        #region addition
        public void RegisterPipeModule(params IResultPipelineModule[] modules)
        {
            _resultPipeline.RegisterModule(modules);
        }

        public void RegisterPageAnalyzer<T>(string siteId) where T : IPageAnalyzer, new()
        {
            _pageAnalyzerTypes.Add(siteId, typeof(T));
        }

        public void RegisterDownloader(IDownloader downloader)
        {
            _downloaders.Add(downloader);
        }

        public void RegisterDownloader(IEnumerable<IDownloader> downloaders)
        {
            _downloaders.AddRange(downloaders);
        }

        public void RegisterMessageProvider(IRequestMessageProvider requestMessageProvider)
        {
            _requestMessageProvider = requestMessageProvider;
            throw new NotImplementedException();
        }

        #endregion

        #region Running and Monitoring

        public SpiderStatus RunStatusInfo()
        {
            var status = new SpiderStatus()
            {
                QueueCount = _scheduler.CurrentQueueCount(),
                TaskCount = _consumerBroker.DownloadingTaskCount(),
                ConsumeTotal = _consumerBroker.ConsumeTotal,
                ResultTotal = _resultPipeline.ResultTotal
            };
            return status;
        }

        #endregion

        public void Start(IEnumerable<RequestMessage> seedRequests)
        {
            _seedRequests.AddRange(seedRequests);
            Run();
        }

        public void Start(string topic, string siteId, string url)
        {
            var seed = new RequestMessage()
            {
                Topic = topic,
                Request = new Request() { SiteId = siteId, Url = url}
            };
            _seedRequests.Add(seed);
            Run();
        }

        private void Run()
        {
            if (!CheckConfig())
                return;
            _consumerBroker = new ConsumerBroker(10, _scheduler, _downloaders, _resultPipeline, _pageAnalyzerTypes);
            _scheduler.PushAsync(_seedRequests);
        }
    }
}
