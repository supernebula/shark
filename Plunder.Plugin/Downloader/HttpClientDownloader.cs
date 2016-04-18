
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Downloader;
using System.Threading;

namespace Plunder.Plugin.Downloader
{
    public class HttpClientDownloader : IDownloader
    {
        private readonly int _maxTaskNumber;
        private int _currentTaskNumber; 
        private readonly SemaphoreSlim _ctnLock = new SemaphoreSlim(1);

        public string Topic => TopicType.StaticHtml;

        public int DownloadingTaskCount
        {
            get { return _currentTaskNumber; }
        }

        public bool IsDefault { get; set; }

        public HttpClientDownloader(int maxTaskNumber)
        {
            _maxTaskNumber = maxTaskNumber;
        }

        public void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloadComplete)
        {
            foreach (Request req in requests)
            {
                Interlocked.Increment(ref _currentTaskNumber);
                Task.Run(async () =>
                {
                    try
                    {
                        var client = HttpClientBuilder.GetClient(req.SiteId);
                        var httpResp = await client.GetAsync(req.Url);
                        var resp = new Response()
                        {
                            Request = req,
                            HttpStatusCode = httpResp.StatusCode,
                            IsSuccessCode = httpResp.IsSuccessStatusCode,
                            ReasonPhrase = httpResp.ReasonPhrase,
                            Content = httpResp.IsSuccessStatusCode ? await httpResp.Content.ReadAsStringAsync() : null
                        };
                        return new Tuple<Request, Response>(req, resp);
                    }
                    finally
                    {
                        Interlocked.Increment(ref _currentTaskNumber);
                    }

                }).ContinueWith(t =>
                {
                    onDownloadComplete(t.Result.Item1, t.Result.Item2);
                });  
            }
        }

        public async Task<Response> DownloadAsync(Request request)
        {
            Interlocked.Increment(ref _currentTaskNumber);
            var result = new Response() {Request = request};
            await _ctnLock.WaitAsync();
            try
            {
                var client = HttpClientBuilder.GetClient(request.SiteId);
                var resposneMessage = await client.GetAsync(request.Url);
                result.HttpStatusCode = resposneMessage.StatusCode;
                result.IsSuccessCode = resposneMessage.IsSuccessStatusCode;
                result.ReasonPhrase = resposneMessage.ReasonPhrase;
                if (resposneMessage.IsSuccessStatusCode)
                {
                    result.Content = await resposneMessage.Content.ReadAsStringAsync();
                }
            }
            finally
            {
                _ctnLock.Release();
                Interlocked.Decrement(ref _currentTaskNumber);
            }
            return result;
        }

        public bool IsAllowDownload()
        {
            return _maxTaskNumber > _currentTaskNumber;
        }
    }
}
