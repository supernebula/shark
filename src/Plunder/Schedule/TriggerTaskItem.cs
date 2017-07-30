using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plunder.Schedule
{
    public class TriggerTaskItem
    {
        public string Id { get; set; }

        public CancellationTokenSource CancelTokenSource { get; set; }

        public Task DownloadTask { get; set; }

        public bool IsComplete { get; set; }
    }
}
