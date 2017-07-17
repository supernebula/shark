
using System.Net.Http;

namespace Plunder.Compoment
{
    public class Request
    {
        public Request()
        {
            HttpMethod = HttpMethod.Get;
        }

        public string SiteId { get; set; }

        public string Channel { get; set; }

        public string Domain { get; set; }

        public string Url { get; set; }

        public string Hash => this.Url.GetHashCode().ToString();

        public UrlType UrlType { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public int RemainRetryCount { get; set; }
    }
}
