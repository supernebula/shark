using System.Net.Http;
using Plunder.Util;
using Plunder.Download;

namespace Plunder.Compoment
{
    public class Request
    {
        public Request(string url)
        {
            Url = url;
            Id = HashUtil.Md5(url);
            HttpMethod = HttpMethod.Get;
            DelaySecond = 3;
        }

        public int DelaySecond { get; set; }

        public string Id { get; private set; }

        public string SiteId { get; set; }

        public string Channel { get; set; }

        public string Domain { get; set; }

        public string Url { get; private set; }

        public PageType PageType { get; set; }

        public string Hash => this.Url.GetHashCode().ToString();

        public UrlType UrlType { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public int RemainRetryCount { get; set; }

        public string Downloader { get; set; }
    }
}
