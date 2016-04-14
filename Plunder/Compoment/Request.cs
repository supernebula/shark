
using System.Net.Http;

namespace Plunder.Compoment
{
    public class Request
    {
        public string SiteId { get; set; }

        public string Topic { get; set; }

        public string Uri { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public int RemainRetryCount { get; set; }
    }
}
