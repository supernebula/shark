using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class RequestMessage
    {

        public Guid Id { get; set; }
        public string Topic { get; set; }

        public Request Request { get; set; }

        public int Priority { get; set; }

        public DateTime Timestamp { get; set; }

        public string HashCode
        {
            get
            {
                return Request.Uri.GetHashCode().ToString().ToLower();
            }
        }

    }
}
