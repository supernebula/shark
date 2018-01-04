using System;
using System.Net.Http;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Download;
using Plunder.Download.Proxy;
using System.Diagnostics;
using Plunder.Util;
using System.Threading;

namespace Plunder.Plugin.Download
{
    public class HttpClientDownloader : IDownloader
    {

        public Request Request { get; private set; }
        public UserAgent UserAgent { get; set; }
        public HttpProxy Proxy { get; set; }

        public PageType PageType { get; private set; }

        public DownloadStatus Status { get; private set; }

        public DateTime? DownloadStartTime { get; private set; }

        private HttpClient _httpClient { get; set; }

        public int? _elapsed;

        public int HasElapsed {
            get
            {
                if (_elapsed != null)
                    return _elapsed.Value;
                if (DownloadStartTime == null)
                    return 0;
                return (int)(DateTime.Now.Ticks - DownloadStartTime.Value.Ticks);
            }
        }

        private int _hasElapsed;

        public HttpClientDownloader(Request request, PageType pageType)
        {
            Request = request;
            PageType = pageType;
            //Id = HashUtil.Md5(this.GetType().FullName + request.Id);
        }

        public async Task<Response> DownloadAsync(CancellationToken token)
        {
            _httpClient = HttpClientBuilder.GetClient(Request);
            var watch = new Stopwatch();
            _httpClient.Timeout = TimeSpan.FromSeconds(60);

            HttpResponseMessage result;

            watch.Start();
            try
            {
                result = await _httpClient.GetAsync(Request.Url, token);
                watch.Stop();
                _elapsed = watch.Elapsed.Milliseconds;
                var resposne = new Response();
                resposne.Request = Request;
                resposne.IsSuccessCode = result.IsSuccessStatusCode;
                resposne.HttpStatusCode = result.StatusCode;
                resposne.ReasonPhrase = result.ReasonPhrase;
                if (!result.IsSuccessStatusCode)
                    return resposne;

                resposne.Content = await result.Content.ReadAsStringAsync();
                resposne.StreamContent = await result.Content.ReadAsStreamAsync();
                resposne.Elapsed = _elapsed.Value;
                resposne.DownloaderType = this.GetType();
                return resposne;
            }
            catch (Exception ex)
            {
                throw;
            }

           

            
        }
    }
}
