using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public interface IMessage
    {
        string Id { get; set; }

        DateTime Timestamp { get; set; }

        int Priority { get; set; }

        string HashCode { get; set; }

        string GetTypeName { get; set; }
    }
}
