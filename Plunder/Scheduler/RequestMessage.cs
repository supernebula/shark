using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class RequestMessage : IMessage<Request>
    {
        public string Id { get; set; }
        public string Topic { get; set; }

        public object Content { get; set; }

        public string GetTypeName { get; set; }

        public string HashCode { get; set; }

        public Request Body { get; }

        public int Priority { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
