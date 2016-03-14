using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Analyze;
using Plunder.Scheduler;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Pipeline;
using Plunder.Proxy;

namespace Plunder
{
    public class Spider
    {

        private readonly List<KeyValuePair<string, string>> _initErrors;

        #region Required Unit

        private readonly IMonitorableScheduler _scheduler;
        private readonly ConsumerBroker _consumerBroker;
        private ResultPipeline _resultPipeline;

        #endregion

        #region Initialization

        public Spider(IMonitorableScheduler scheduler)
        {
            _initErrors = new List<KeyValuePair<string, string>>();
            _scheduler = scheduler;
            _resultPipeline = new ResultPipeline();
            _resultPipeline.RegisterModule(new ProducerModule(_scheduler));

        }

        private bool CheckConfig()
        {
            if (DownloaderFactory.Count() > 0)
                return false;
            return true;
        }

        #endregion

        #region addition

        private readonly ConcurrentDictionary<string, Type> _pageAnalyzerDic;

        public void RegisterPipeModule(params IResultPipelineModule[] modules)
        {
            _resultPipeline.RegisterModule(modules);
        }

        public void RegisterDownloader(string topic, Func<string, IDownloaderBak> downloaderCreateFunc)
        {
            DownloaderFactory.RegisterCreator(topic, downloaderCreateFunc);
        }


        public void RegisterPageAnalyzer<T>(string name) where T : IPageAnalyzer
        {

            _pageAnalyzerDic.TryAdd(name, typeof(T));
        }

        #endregion

        #region Running and Monitoring

        public string RunStatusInfo()
        {
            throw new NotImplementedException();
        }

        #endregion


        public void Start()
        {
            if (!CheckConfig())
                return;
            _scheduler.PushAsync(new RequestMessage());
        }


    }
}
