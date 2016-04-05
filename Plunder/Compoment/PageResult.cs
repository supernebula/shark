using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class PageResult
    {
        public Request Request { get; set; }

        public Response Response { get; set; }

        public IEnumerable<Request> NewRequests { get; set; }

        public string Channel { get; set; }

        public IEnumerable<ResultField> Data { get; set; }
    }


    public class ResultField
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
