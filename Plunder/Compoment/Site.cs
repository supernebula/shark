using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Compoment
{
    public class Site
    {
        public string Domain { get; set; }

        public string UserAgent { get; set; }

        public List<KeyValuePair<string, string>> DefaultCookie { get; set; }

        public UserAgent UserAgent { get; set; }
    }
}
