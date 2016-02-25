using Plunder.Downloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class CustomerManager
    {
        Dictionary<string, ICustomerPool<IDownloader>> _customerPoolGroup;
        public CustomerManager()
        {
            _customerPoolGroup = new Dictionary<string, ICustomerPool<IDownloader>>() {
                { "Simple",new HttpSimpleDownloaderPool()},
                { "Dynamic",new HttpDynamicDownloadPool()}
            };
        }


        public void PullMessage()
        {
            //pull message from queue
            throw new NotImplementedException();

        }

        private void Consume(IMessage<Request> messge)
        {
            
            var pool = _customerPoolGroup[messge.Topic];
            IDownloader downloader = pool.Take();
            downloader.Download(messge.Body);
        }
    }
}
