using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Proxy
{
    public class ProxyPool : IPool
    {
        private static ProxyPool _instance;
        public static ProxyPool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ProxyPool();
                return _instance;
            }
        }

        public HttpProxy Random()
        {
            throw new NotImplementedException();
        }
    }
}
