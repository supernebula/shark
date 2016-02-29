using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IConsumer
    {
        Guid Id { get; set; }
        string Topic { get; set; }

        bool IsBusy { get; set; }
    }
}
