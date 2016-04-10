using System;
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
        private readonly SemaphoreSlim _ctnLock = new SemaphoreSlim(1); //异步锁

        public string Topic => _topic;

        public bool IsDefault { get; set; }

        public HttpClientDownloader(string topic, int maxTaskNumber)
        {
            _topic = topic;
            _maxTaskNumber = maxTaskNumber;
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

        public int TaskCount()
        {
            throw new NotImplementedException();
        }
    }
}
