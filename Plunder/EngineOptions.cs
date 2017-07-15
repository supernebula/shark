using Plunder.Pipeline;
using Plunder.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder
{
    public class EngineOptions
    {
        public IMonitorableScheduler Scheduler { get; set; }

        public DownloaderFactory DownloaderFactory { get;set;}

        public PageAnalyzerFactory PageAnalyzerFactory { get; set; }

        public ResultItemPipeline ResultPipeline { get; set; }

        ////以下确定

        //private ConsumerBroker _consumerBroker;
        //private List<RequestMessage> _seedRequests;
        //private IRequestMessageProvider _requestMessageProvider;
    }
}
