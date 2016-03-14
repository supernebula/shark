using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Plunder.Analyze;
using Plunder.Scheduler;
using Plunder.Downloader;
using Plunder.Pipeline;

namespace Plunder
{
    public class Spider
    {
        #region Required Unit

        private readonly IMonitorableScheduler _scheduler;
        private readonly ConsumerBroker _consumerBroker;
        private readonly ResultPipeline _resultPipeline;
        private IEnumerable<RequestMessage> _seedRequests;
        private readonly ConcurrentDictionary<string, Type> pageAnalyzerTypes;

        #endregion

        #region Initialization

        public Spider(IMonitorableScheduler scheduler, IEnumerable<IDownloader> downloaders)
        {
            _scheduler = scheduler;
            _resultPipeline = new ResultPipeline();
            _resultPipeline.RegisterModule(new ProducerModule(_scheduler)); //ProducerModule is required
            _consumerBroker = new ConsumerBroker(10, _scheduler, downloaders, _resultPipeline, pageAnalyzerTypes);
            
        }

        private bool CheckConfig()
        {
            //if (DownloaderFactory.Count() > 0)
            //    return false;
            return true;
        }

        #endregion

        #region addition

        

        public void RegisterPipeModule(params IResultPipelineModule[] modules)
        {
            _resultPipeline.RegisterModule(modules);
        }



        public void RegisterPageAnalyzer<T>(string name) where T : IPageAnalyzer
        {

            pageAnalyzerTypes.TryAdd(name, typeof(T));
        }

        #endregion

        #region Running and Monitoring

        public string RunStatusInfo()
        {
            throw new NotImplementedException();
        }

        #endregion


        public void Start(IEnumerable<RequestMessage> seedRequests)
        {
            _seedRequests = seedRequests;
            if (!CheckConfig())
                return;
            _scheduler.PushAsync(_seedRequests);
        }


    }
}
