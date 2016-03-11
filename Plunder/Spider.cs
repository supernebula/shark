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

        private readonly IMonitorableScheduler _scheduler;
        private readonly ConcurrentDictionary<string, Type> _pageAnalyzerDic;
        private ResultPipeline _resultPipeline;

        public Spider(IMonitorableScheduler scheduler)
        {
            _initErrors = new List<KeyValuePair<string, string>>();
            _scheduler = scheduler;
            _resultPipeline = new ResultPipeline();
            _resultPipeline.RegisterModule(new ProducerModule(_scheduler));
            
        }

        public void RegisterPipeModule(params IResultPipelineModule[] modules)
        {
            _resultPipeline.RegisterModule(modules);
        }

        public void RegisterDownloader(string topic, Func<string, IDownloader> downloaderCreateFunc)
        {
            DownloaderFactory.RegisterCreator(topic, downloaderCreateFunc);
        }


        public void RegisterPageAnalyzer<T>(string name) where T : IPageAnalyzer
        {

            _pageAnalyzerDic.TryAdd(name, typeof (T));
        }

        private bool CheckConfig()
        {
            if (DownloaderFactory.Count() > 0)
                return false;
            return true;
        }

        public string RunStatusInfo()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            if (!CheckConfig())
                return;
            _scheduler.PushAsync(new RequestMessage());
        }


    }
}
