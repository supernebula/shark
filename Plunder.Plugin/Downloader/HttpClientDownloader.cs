
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private HttpClientBuilder _httpClientBuilder;
        private HttpProxyPool _proxyPool;

        public string Topic => _topic;

        public HttpClientDownloader(string topic, int maxTaskNumber, HttpProxyPool proxyPool)
        {
            _topic = topic;
            _maxTaskNumber = maxTaskNumber;
            _proxyPool = proxyPool;
            _httpClientBuilder = new HttpClientBuilder();
        }


        public async Task<string> DownloadAsync(Request request)
        {
            string result = null;
            await _ctnLock.WaitAsync();
            try
            {
                var client = _httpClientBuilder.GeClient(request.Site);
                _currentTaskNumber++;
                var resposne = await client.GetAsync(request.Uri);
                if (resposne.IsSuccessStatusCode)
                    result = await resposne.Content.ReadAsStringAsync();
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
