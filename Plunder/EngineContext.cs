using Plunder.Core;
using Plunder.Schedule;

namespace Plunder
{
    public class EngineContext
    {
        public IMonitorableScheduler Scheduler { get; set; }

        public IDownloaderFactory DownloaderFactory { get; set; }

        public IPageAnalyzerFactory PageAnalyzerFactory { get; set; }

        public IResultItemPipeline ResultPipeline { get; set; }
    }
}
