using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public class HttpSimpleDownloader : AbstractConsumer, IDownloader
    {
        private IMessage<Request> _message;
        private Request _reqeust;
        private HttpProxy _proxy;

        public HttpSimpleDownloader(Guid id)
        {
            Id = id;
        }

        public void Init(IMessage<Request> requestMessage, HttpProxy proxy)
        {
            _message = requestMessage;
            _reqeust = _message.Body;
            _proxy = proxy;
        }

        private HttpClient Client()
        {
            var httpClient = new HttpClient();
            return httpClient;

        }


        public async Task Download()
        {

            var task = await Client().GetAsync(_reqeust.Uri);
            if (!task.IsSuccessStatusCode)
            
            throw new NotImplementedException();
        }


    }
}
