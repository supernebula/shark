
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Proxy;
using Plunder.Downloader;
using System.Threading;

namespace Plunder.Plugin.Downloader
{
    public class HttpClientDownloader : IDownloader
    {
        private readonly string _topic;
        private readonly int _maxTaskNumber;
        private int _currentTaskNumber;
        private readonly SemaphoreSlim _ctnLock = new SemaphoreSlim(1);
        private HttpProxyPool _proxyPool;

        public string Topic => _topic;
        private List<int> _doingTask;

        public int DownloadingTaskCount
        {
            get { return _doingTask.Count; }
        }

        public HttpClientDownloader(string topic, int maxTaskNumber, HttpProxyPool proxyPool)
        {
            _topic = topic;
            _maxTaskNumber = maxTaskNumber;
            _proxyPool = proxyPool;
            _doingTask = new List<int>();
        }


        public void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloadComplete)
        {
            foreach (Request req in requests)
            {
                var task = Task.Run(async () =>
                {
                    var client = HttpClientBuilder.GetClient(req.Site);
                    var httpResp = await client.GetAsync(req.Uri);
                    var resp = new Response()
                    {
                        Request = req,
                        HttpStatusCode = httpResp.StatusCode,
                        IsSuccessCode = httpResp.IsSuccessStatusCode,
                        ReasonPhrase = httpResp.ReasonPhrase,
                        Content = httpResp.IsSuccessStatusCode ? await httpResp.Content.ReadAsStringAsync() : null
                    };
                    return new Tuple<Request, Response>(req, resp);

                }).ContinueWith((t) =>
                {
                    _doingTask.Remove(t.Id);
                    onDownloadComplete(t.Result.Item1, t.Result.Item2);
                });
                _doingTask.Add(task.Id);

            }
        }


        public async Task<Response> DownloadAsync(Request request)
        {
            var result = new Response() {Request = request};
            await _ctnLock.WaitAsync();
            try
            {
                var client = HttpClientBuilder.GetClient(request.Site);
                _currentTaskNumber++;
                var resposneMessage = await client.GetAsync(request.Uri);
                result.HttpStatusCode = resposneMessage.StatusCode;
                result.IsSuccessCode = resposneMessage.IsSuccessStatusCode;
                result.ReasonPhrase = resposneMessage.ReasonPhrase;
                if (resposneMessage.IsSuccessStatusCode)
                {
                    result.Content = await resposneMessage.Content.ReadAsStringAsync();
                }
                _currentTaskNumber--;
            }
            finally
            {
                _ctnLock.Release();
            }
            return result;
        }

        public bool IsAllowDownload()
        {
            return _maxTaskNumber > _currentTaskNumber;
        }
    }
}
