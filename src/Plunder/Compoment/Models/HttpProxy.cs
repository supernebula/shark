using Plunder.Compoment.Values;
using System;

namespace Plunder.Compoment.Models
{
    public class HttpProxy
    {
        public Guid Id { get; set; }

        public HttpProxyType ProxyType { get; set; }

        public string Ip { get; set; }

        public string Port { get; set; }

        public string Location { get; set; }

        public string ResponseSecond { get; set; }

        public string LastVerifyTime { get; set; }

        public string Source { get; set; }
    }
}
