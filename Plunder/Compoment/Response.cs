using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class Response
    {
        public Request Request { get; set; }
        public string HTTPStatusCode { get; set; }

        public bool IsSuccessCode { get; set; }

        public string Content { get; set; }

        public int MillisecondTime { get; set; }
    }
}
