using System;
using Plunder.Compoment;


namespace Plunder.Scheduler
{
    public class RequestMessage
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }

        public Request Request { get; set; }

        public int Priority { get; set; }

        public DateTime Timestamp { get; set; }

        public string UniqueValue => Request.Uri.ToLower();
    }
}
