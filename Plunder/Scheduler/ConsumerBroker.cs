using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Proxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plunder.Scheduler
{
    public class ConsumerBroker
    {
        IScheduler _scheduler;

        ConcurrentDictionary<Guid, IDownloader> _backupConsumers; 

        ConcurrentDictionary<Guid, IDownloader> _activityConsumers; 

        Timer _gcTimer;
        Timer _messagePullTimer;

        bool _pulling = false;

        int _minConsumerNumber;
        int _maxConsumerNumber;

        #region Initialization

        public ConsumerBroker(IScheduler scheduler, int minConsumerNumber, int maxConsumerNumber)
        {
            if (minConsumerNumber > maxConsumerNumber)
                throw new ArgumentException("参数 maxConsumerNumber 必须大于或等于 minConsumerNumber");
            _scheduler = scheduler;
            _minConsumerNumber = minConsumerNumber;
            _maxConsumerNumber = maxConsumerNumber;

            _gcTimer = new Timer(GcConsumer, null, 60 * 1000, 10 * 1000);
            _messagePullTimer = new Timer(PullMessage, null, 0, 1000);

            InitConsumers();
        }

        private void InitConsumers()
        {
            _activityConsumers = new ConcurrentDictionary<Guid, IDownloader>();
            for (int i = 0; i < _minConsumerNumber; i++)
            {
                var downloader = CreateDownloader("simpleDownload");
                _activityConsumers.TryAdd(downloader.Id, downloader);

                var downloader2 = CreateDownloader("dynamicDownload");
                _activityConsumers.TryAdd(downloader.Id, downloader2);
            }

            _backupConsumers = new ConcurrentDictionary<Guid, IDownloader>();
        }

        private void GcConsumer(object state)
        {
            var _backupGCIds = CanClearIds(_backupConsumers, IdleGeneration.VERY_IDLE);
            foreach (var id in _backupGCIds)
            {
                IDownloader temp;
                _backupConsumers.TryRemove(id, out temp);
            }

            var _activityGCIds = CanClearIds(_activityConsumers, IdleGeneration.IDLE);
            foreach (var id in _activityGCIds)
            {
                IDownloader temp;
                _activityConsumers.TryRemove(id, out temp);
                _backupConsumers.TryAdd(temp.Id, temp);
            }


        }

        private List<Guid> CanClearIds(ConcurrentDictionary<Guid, IDownloader> dic, int idleGeneration)
        {
            var ids = new List<Guid>();
            foreach (IConsumer item in dic.Values)
            {
                if (item.IsBusy)
                    continue;
                item.IdleGeneration++;
                if (item.IdleGeneration >= idleGeneration)
                    ids.Add(item.Id);
            }
            return ids;
        }

        #endregion

        #region message

        private void PullMessage(object state)
        {
            if (_pulling)
                return;
            _pulling = true;
            var message = _scheduler.Poll();
            _pulling = false;
            Consume(message as IMessage<Request>); 
        }

        private void Consume(IMessage<Request> message)
        {
            var downloader = FetchDownloader(message.Topic);
            downloader.Init(message, ProxyPool.Instance.Random());
            var task = downloader.Download();
            Task.WhenAny(task);
        }

        #endregion

        private IDownloader FetchDownloader(string topic)
        {
            var d = _activityConsumers.FirstOrDefault(e => !e.Value.IsBusy).Value;
            if(d == null)
                d = _backupConsumers.FirstOrDefault().Value;
            if (d == null)
                d = CreateDownloader(topic);
            d.IdleGeneration = IdleGeneration.ACTIVE;
            _activityConsumers.TryAdd(d.Id, d);
            return d;
        }

        private IDownloader CreateDownloader(string topic)
        {
            IDownloader downloader;
            switch (topic)
            {
                case "simpleDownload":
                    downloader = new HttpSimpleDownloader(Guid.NewGuid());
                    break;
                case "dynamicDownload":
                    downloader = new HttpSimpleDownloader(Guid.NewGuid());
                    break;
                default:
                    throw new InvalidCastException("无效的topic");                    
            }
            return downloader;
        }

        public void Start()
        {
            PullMessage(null);
        }
    }
}
