using System;
using Plunder.Compoment;

namespace Plunder.Schedule
{
    public class RequestMessage
    {
        public string Topic { get; set; }

        public Request Request { get; set; }

        public int Priority { get; set; }

        public DateTime Timestamp { get; set; }

        public string UniqueValue => Request.Url.ToLower();
    }
}
