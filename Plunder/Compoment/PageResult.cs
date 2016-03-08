using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class PageResult<T>
    {
        public Site Site { get; set; }
        public List<Request> NewRequests { get; set; }

        public T Model { get; set; }

        public string Content { get; set; }

        public int HttpStatCode { get; set; }


    }
}
