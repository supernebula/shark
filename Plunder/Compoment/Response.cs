using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class Response
    {
        public Request Request { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public bool IsSuccessCode { get; set; }

        public string Content { get; set; }

        public int MillisecondTime { get; set; }
    }
}
