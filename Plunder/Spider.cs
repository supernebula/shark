using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private bool CheckConfig(out string error)
        {
            Console.WriteLine("检查配置");
            var checkInfo = new StringBuilder();
            if (_scheduler == null)
                checkInfo.AppendLine("Error:缺少具体的Scheduler");
            if (_resultPipeline == null)
                checkInfo.AppendLine("Error:缺少ResultPipeline");
            else
            {
                if (_resultPipeline.ModuleCount == 0)
                    checkInfo.AppendLine("Error:ResultPipeline没有包含任何Module");
                if (!_resultPipeline.IsContainProducer())
                    checkInfo.AppendLine("Error:ResultPipeline缺少ProducerModule");
            }

            if (_downloaders == null || !_downloaders.Any())
                checkInfo.AppendLine("Error:缺少Downloader");

            if (_pageAnalyzerTypes == null || !_pageAnalyzerTypes.Any())
                checkInfo.AppendLine("Error:缺少PageAnalyzer");

            error = checkInfo.ToString();
            return checkInfo.Length == 0;
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
            _seedRequests.AddRange(seeds);
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

            _consumerBroker = new ConsumerBroker(100, _scheduler, _downloaders, _resultPipeline, _pageAnalyzerTypes);
            _scheduler.Push(_seedRequests);
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
