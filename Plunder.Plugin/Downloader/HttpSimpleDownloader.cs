using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plunder.Downloader;

namespace Plunder.Plugin.Downloader
{
    public class HttpSimpleDownloader : AbstractConsumer, IDownloader
    {
        private IMessage<Request> _message;
        private Request _reqeust;
        private HttpProxy _proxy;

        public Site Site { get; set; }

        public HttpSimpleDownloader()
        {
            Topic = TopicType.STATIC_HTML;
        }

        public void Init(Guid id, IMessage<Request> requestMessage, HttpProxy proxy)
        {
            Id = id;
            _message = requestMessage;
            _reqeust = _message.Body;
            _proxy = proxy;
        }

        private HttpClient Client()
        {
            var httpClient = new HttpClient();
            return httpClient;

        }


        public Task<bool> DownloadAsync()
        {
            //var task = await Client().GetAsync(_reqeust.Uri);
            //if (!task.IsSuccessStatusCode)
            
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
