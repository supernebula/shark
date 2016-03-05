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
        private readonly List<IPageResultModule> _moduleList; 

        public Spider(IMonitorableScheduler scheduler)
        {
            _scheduler = scheduler;
            _moduleList = new List<IPageResultModule>();
        }

        public void AddPipeLineModule(params IPageResultModule[] module)
        {
            _moduleList.AddRange(module);
        }

        public void RegisterDownloader(string topic, Func<string, IDownloader> downloaderCreateFunc)
        {
            DownloaderFactory.RegisterCreator(topic, downloaderCreateFunc);
        }

        public void RegisterDownloader<DT>(string topic)
        {

        }

        public bool CheckConfig()
        {

            if (DownloaderFactory.Validate())
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
