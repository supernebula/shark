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



        public void Start()
        {
            _scheduler.Push(new RequestMessage());
        }
    }
}
