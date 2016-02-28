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
        IScheduler _scheduler;
        public CustomerManager(IScheduler scheduler)
        {
            _scheduler = scheduler;
            _customerPoolGroup = new Dictionary<string, ICustomerPool<IDownloader>>() {
                { "Topic.SimpleHtml",new HttpSimpleDownloaderPool()},
                { "Topic.DynamicHtml",new HttpDynamicDownloadPool()}
            };
        }

        private void Consume(IMessage<Request> message)
        {
            var pool = _customerPoolGroup[message.Topic];
            IDownloader downloader = pool.Take();
            downloader.Download(message.Body);
        }

        private void RunConsume()
        {
            var message = _scheduler.Poll();
            Consume((IMessage<Request>)message);
        }

        public void Run()
        {
            RunConsume();
        }
    }
}
