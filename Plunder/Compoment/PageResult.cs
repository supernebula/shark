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
        public Site Site { get; set; }
        public IEnumerable<Request> NewRequests { get; set; }

        public IEnumerable<ResultField> Result { get; set; }


        public string Content { get; set; }

        public HttpStatusCode HttpStatCode { get; set; }


    }


    public class ResultField
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
