using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Proxy;


namespace Plunder.Scheduler
{
    public class ConsumerBroker : IMonitorableBroker, IDisposable
    {
        private readonly IMonitorableScheduler _scheduler;

        private readonly ConcurrentDictionary<Guid, IDownloader> _backupConsumers;

        private readonly ConcurrentDictionary<Guid, IDownloader> _activityConsumers;

        private readonly Timer _gcTimer;
        private readonly Timer _messagePullTimer;

        private bool _pulling;

        private readonly int _minConsumerNumber;
        private readonly int _maxConsumerNumber;

        #region Initialization

        public ConsumerBroker(IMonitorableScheduler scheduler, int minConsumerNumber, int maxConsumerNumber)
        {
            if (minConsumerNumber > maxConsumerNumber)
                throw new ArgumentException("参数 maxConsumerNumber 必须大于或等于 minConsumerNumber");
            _scheduler = scheduler;
            _minConsumerNumber = minConsumerNumber;
            _maxConsumerNumber = maxConsumerNumber;

            _gcTimer = new Timer(GcConsumer, null, 60 * 1000, 10 * 1000);
            _messagePullTimer = new Timer(PullMessage, null, 0, 1000);

            //Init Consumers 
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
            IDownloader temp;
            foreach (var item in _backupConsumers.Values)
            {
                if (!item.IsBusy)
                    item.IdleGeneration++;
                if(item.IdleGeneration >= IdleGeneration.VERY_IDLE)
                    _backupConsumers.TryRemove(item.Id, out temp);
            }

            if (_activityConsumers.Count <= _minConsumerNumber)
                return;
            foreach (var item in _activityConsumers.Values)
            {
                if (!item.IsBusy)
                    item.IdleGeneration++;
                if (item.IdleGeneration >= IdleGeneration.IDLE && _activityConsumers.TryRemove(item.Id, out temp))
                    _backupConsumers.TryAdd(temp.Id, temp);
            }
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

        private async void Consume(IMessage<Request> message)
        {
            var downloader = FetchDownloader(message.Topic);
            if (downloader == null)
                return;
            downloader.Init(message, ProxyPool.Instance.Random());
            await downloader.DownloadAsync();
            downloader.IdleGeneration = IdleGeneration.JUST_FINISHED;
            PullMessage(null);
        }

        #endregion

        private IDownloader FetchDownloader(string topic)
        {
            var d = _activityConsumers.FirstOrDefault(e => !e.Value.IsBusy).Value;
            if(d == null)
                d = _backupConsumers.FirstOrDefault().Value;
            if ((_activityConsumers.Count + _backupConsumers.Count) >= _maxConsumerNumber)
                return null;
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

        public void Dispose()
        {
            _gcTimer.Dispose();
            _messagePullTimer.Dispose();
        }

        public int BusyWorkerNumber()
        {
            return _activityConsumers.Values.Count(e => e.IsBusy);
        }

        public int AllWorkerNumber()
        {
            return _activityConsumers.Count + _backupConsumers.Count;
        }
    }
}
