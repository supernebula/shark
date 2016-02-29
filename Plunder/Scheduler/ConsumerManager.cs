using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class ConsumerManager
    {
        IScheduler _scheduler;
        Dictionary<Guid, IDownloader> _consumers;
        int _maxConsumerNumber;
        public ConsumerManager(IScheduler scheduler, int maxConsumerNumber)
        {
            _scheduler = scheduler;
            _maxConsumerNumber = maxConsumerNumber;
        }

        private IDownloader CreateDownloader(string topic)
        {
            IDownloader downloader;
            switch (topic)
            {
                case "simpleDownload":
                    downloader = new HttpSimpleDownloader();
                    break;
                case "dynamicDownload":
                    downloader = new HttpSimpleDownloader();
                    break;
                default:
                    throw new InvalidCastException("无效的topic");                    
            }
            _consumers.Add(downloader.Id, downloader);
            return downloader;
        }

        private void Consume(IMessage<Request> message)
        {
            var downloader = CreateDownloader(message.Topic);
            downloader.Init(message, ProxyPool.Instance.Random());
            var task = downloader.Download();
            Task.WhenAny(task);
        }

        private void RunConsume()
        {
            var message = _scheduler.Poll();
            Consume((IMessage<Request>)message);
        }

        public void Start()
        {
            RunConsume();
        }
    }
}
