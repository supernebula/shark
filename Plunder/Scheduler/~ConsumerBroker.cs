using System;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Plunder.Compoment;
using Plunder.Downloader;
using Plunder.Proxy;
using System.Collections.Generic;

namespace Plunder.Scheduler 
{
    [Obsolete]
    public class ConsumerBrokerBak : IMonitorableBroker, IDisposable
    {
        private readonly IMonitorableScheduler _scheduler;

        private readonly Dictionary<string, Type> DownloaderType;

        private readonly ConcurrentDictionary<Guid, IDownloaderBak> _backupConsumers;

        private readonly ConcurrentDictionary<Guid, IDownloaderBak> _activityConsumers;

        private readonly Timer _gcTimer;
        private readonly Timer _messagePullTimer;

        private bool _pulling;

        private readonly int _minConsumerNumber;
        private readonly int _maxConsumerNumber;

        #region Initialization

        public ConsumerBrokerBak(IMonitorableScheduler scheduler, int minConsumerNumber, int maxConsumerNumber)
        {
            if (minConsumerNumber > maxConsumerNumber)
                throw new ArgumentException("参数 maxConsumerNumber 必须大于或等于 minConsumerNumber");
            _scheduler = scheduler;
            _minConsumerNumber = minConsumerNumber;
            _maxConsumerNumber = maxConsumerNumber;

            _gcTimer = new Timer(GcConsumer, null, 60 * 1000, 10 * 1000);
            _messagePullTimer = new Timer(PullMessage, null, 0, 1000);

            //Init Consumers 
            _activityConsumers = new ConcurrentDictionary<Guid, IDownloaderBak>();
            for (int i = 0; i < _minConsumerNumber; i++)
            {
                var downloader = CreateDownloader("simpleDownload");
                _activityConsumers.TryAdd(downloader.Id, downloader);

                var downloader2 = CreateDownloader("dynamicDownload");
                _activityConsumers.TryAdd(downloader.Id, downloader2);
            }
            _backupConsumers = new ConcurrentDictionary<Guid, IDownloaderBak>();
        }

        private void GcConsumer(object state)
        {
            IDownloaderBak temp;
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
            downloader.Init(Guid.NewGuid(), message, HttpProxyPool.Instance.Random());
            await downloader.DownloadAsync();
            downloader.IdleGeneration = IdleGeneration.JUST_FINISHED;
            PullMessage(null);
        }

        #endregion

        private IDownloaderBak FetchDownloader(string topic)
        {
            var d = _activityConsumers.FirstOrDefault(e => !e.Value.IsBusy && e.Value.Topic.Equals(topic)).Value;
            if(d == null)
                d = _backupConsumers.FirstOrDefault(e => e.Value.Topic.Equals(topic)).Value;
            if ((_activityConsumers.Count + _backupConsumers.Count) >= _maxConsumerNumber)
                return null;
            if (d == null)
                d = CreateDownloader(topic);
            d.IdleGeneration = IdleGeneration.ACTIVE;
            _activityConsumers.TryAdd(d.Id, d);
            return d;
        }

        private IDownloaderBak CreateDownloader(string topic)
        {

            var type = DownloaderType[topic];
            if(type == null)
                throw new ArgumentException("未注册此topic对应的Downloader:" + topic);
            var downloader = (IDownloaderBak)TypeDescriptor.CreateInstance(null, type, null, null);
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
