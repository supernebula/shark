using System;
using System.Collections.Generic;

namespace Plunder
{
    public class CrawlerStatus
    {
        public int QueueCount { get; set; }

        public int TaskCount { get; set; }

        public List<string> PipeModuleName { get; set; }

        public List<string> DownloaderName { get; set; }

        public List<string> PageAnalyzerName { get; set; }

        public int ConsumeTotal { get; set; }

        public int ResultTotal { get; set; }

        public override string ToString()
        {
            return String.Format("Queue count:{0}\r\nTask count:{1}\r\nConsume total:{2}\r\nResult total:{3}", QueueCount, TaskCount, ConsumeTotal, ResultTotal);
        }
    }
}
