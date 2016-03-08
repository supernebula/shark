using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Scheduler;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Pipeline;
using Plunder.Proxy;

namespace Plunder
{
    public class Spider
    {



        private readonly IMonitorableScheduler _scheduler;
        private ResultPipeline _resultPipeline;

        public Spider(IMonitorableScheduler scheduler)
        {
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

        public void RegisterPageAnalyzer(object analyzer)
        {
            
        }

        public bool CheckConfig()
        {

            if (DownloaderFactory.Count() > 0)
                return false;
                
            return true;
        }

        public void Start()
        {
            if (!CheckConfig())
                return;
            _scheduler.PushAsync(new RequestMessage());
        }
    }
}
