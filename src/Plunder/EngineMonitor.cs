using System;
using System.Collections.Generic;

namespace Plunder
{
    public class EngineMonitor
    {
        public int QueueCount { get; set; }

        public int TaskCount { get; set; }

        public List<string> PipeModuleName { get; set; }

        public List<string> DownloaderName { get; set; }

        public List<string> PageAnalyzerName { get; set; }

        public int ConsumeTotal { get; set; }

        public int ModuleTotal { get; set; }

        public override string ToString()
        {
            return String.Format("Queue count:{0}\r\nTask count:{1}\r\nConsume total:{2}\r\nModule total:{3}", QueueCount, TaskCount, ConsumeTotal, ModuleTotal);
        }
    }
}
