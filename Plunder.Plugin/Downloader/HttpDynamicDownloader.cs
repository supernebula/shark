using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Downloader;

namespace Plunder.Plugin.Downloader
{
    public class HttpDynamicDownloader : AbstractConsumer, IDownloader
    {
        public Site Site { get; set; }

        public HttpDynamicDownloader(Guid id)
        {
            Id = id;
        }

        public void Init(IMessage<Request> request, HttpProxy proxy)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DownloadAsync()
        {
            throw new NotImplementedException();
        }

        public void SetProxy(HttpProxy proxy)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
