using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class AbstractMessage : IMessage
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public int Priority { get; set; }

        public object Content { get; set; }

        public string GetTypeName { get; set; }

        public string HashCode { get; set; }


        public DateTime Timestamp { get; set; }

        
    }
}
