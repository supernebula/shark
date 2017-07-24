using Plunder.Download;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Download.Proxy;
using System.Net.Http;
using System.Net;

namespace Plunder.Plugin.Download
{
    public class HttpClientDownloader : IDownloader
    {
        public Request Request { get; set; }
        public UserAgent UserAgent { get; set; }
        public HttpProxy Proxy { get; set; }

        public PageType PageType { get; private set; }

        public DownloadStatus Status { get; private set; }

        public DateTime? StartDownloadTime { get; private set; }

        private HttpClient _httpClient { get; set; }

        public HttpClientDownloader(PageType pageType)
        {
            PageType = pageType;
        }

        public Task<Response> DownloadAsync()
        {
            _httpClient = new HttpClient();
            throw new NotImplementedException();
        }
    }
}
