using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public class HttpDynamicDownloader : AbstractConsumer, IDownloader
    {
        public HttpDynamicDownloader(Guid id)
        {
            Id = id;
        }

        public void Init(IMessage<Request> request, HttpProxy proxy)
        {
            throw new NotImplementedException();
        }
        public async Task Download()
        {
            throw new NotImplementedException();
        }


    }
}
