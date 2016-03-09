using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class PageResult
    {
        public Site Site { get; set; }
        public List<Request> NewRequests { get; set; }

        public Dictionary<string, string> Result { get; set; }


        public string Content { get; set; }

        public int HttpStatCode { get; set; }


    }
}
