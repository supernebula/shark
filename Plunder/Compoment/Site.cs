using Plunder.Compoment;
using Plunder.Proxy;
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

        public List<KeyValuePair<string, string>> Cookies { get; set; }

        public string Charset { get; set; }

        public List<Request> StartRequests { get; set; }

        public int SleepMilliseconds { get; set; }

        public int RetryTimes { get; set; }

        public int CycleRetryTimes { get; set; }

        public int RetrySleepMilliseconds { get; set; }

        public int TimeOut { get; set; }

        public List<int> AcceptHttpStatCode { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public HttpProxy HttpProxy { get; set; }
        public HttpProxyPool HttpProxyPool { get; set; }

        public bool UseGzip { get; set; }


        public Site()
        {
            AcceptHttpStatCode = new List<int> { 200 };
        }

        public void AddCookie(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void AddCookie(string domain, string name, string value)
        {
            throw new NotImplementedException();
        }


    }
}
