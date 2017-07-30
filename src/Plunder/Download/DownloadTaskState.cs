using System.Collections.Generic;
using Plunder.Compoment;
using Plunder.Schedule;

namespace Plunder.Download
{
    public class DownloadTaskState
    {
        public SchedulerContext Context { get; set; }

        public Request Request {get;set;}
    }
}
