using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class Request
    {
        public Site Site { get; set; }

        public string Uri { get; set; }

        public string Method { get; set; }

        public string Priority { get; set; }

        public int AllowedRetryCount { get; set; }

        public bool UseProxy { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
