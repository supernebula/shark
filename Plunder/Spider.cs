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
        private readonly Dictionary<Type, IResultPipelineModule> _moduleDic;
        private readonly ResultPipeline _resultPipeline;

        public Spider(IMonitorableScheduler scheduler)
        {
            _scheduler = scheduler;
            _moduleDic = new Dictionary<Type, IResultPipelineModule>();
            _moduleDic.Add(typeof(ProducerModule), new ProducerModule());
            _resultPipeline = new ResultPipeline();
        }

        public void RegisterPipeModule(params IResultPipelineModule[] modules)
        {
            foreach (var module in modules)
            {
                _moduleDic.Add(module.GetType(), module);
            }
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
            _scheduler.Push(new RequestMessage());
        }
    }
}
