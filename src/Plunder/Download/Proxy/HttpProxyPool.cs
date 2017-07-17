using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Download.Proxy
{
    public class HttpProxyPool : IPool
    {
        private static HttpProxyPool _instance;
        public static HttpProxyPool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HttpProxyPool();
                return _instance;
            }
        }

        public HttpProxy RandomProxy()
        {
            throw new NotImplementedException();
        }
    }
}
