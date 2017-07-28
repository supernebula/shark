using Plunder.Core;

namespace Plunder.Schedule
{
    public class SchedulerContext
    {
        public SchedulerContext(IMonitorableScheduler scheduler,
            IDownloaderFactory downloaderFactory,
            IResultItemPipeline resultItemPipeline,
            IPageAnalyzerFactory pageAnalyzerFactory)
        {
            Scheduler = scheduler;
            DownloaderFactory = downloaderFactory;
            ResultPipeline = resultItemPipeline;
            PageAnalyzerFactory = pageAnalyzerFactory;
        }

        public IMonitorableScheduler Scheduler { get; private set; }

        public IDownloaderFactory DownloaderFactory { get; private set; }

        public IResultItemPipeline ResultPipeline { get; private set; }

        public IPageAnalyzerFactory PageAnalyzerFactory { get; private set; }
    }
}
