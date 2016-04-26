
using System.Net.Http;

namespace Plunder.Compoment
{
    public class Request
    {
        public string SiteId { get; set; }

        public string Channel { get; set; }

        public string Url { get; set; }

        public UrlType UrlType { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public int RemainRetryCount { get; set; }
    }
}
