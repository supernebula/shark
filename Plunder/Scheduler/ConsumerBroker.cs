using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Plunder.Downloader;



namespace Plunder.Scheduler
{
    public class ConsumerBroker
    {
        private readonly IMonitorableScheduler _scheduler;
        private readonly Dictionary<string, IDownloader> _downloaders;
        private readonly int _maxDownloaderNumber;

        private readonly Timer _messagePullTimer;

        private bool _pulling;

        public ConsumerBroker(IMonitorableScheduler scheduler, IEnumerable<IDownloader> downloaders, int maxDownloaderNumber)
        {
            _scheduler = scheduler;
            _downloaders = new Dictionary<string, IDownloader>();
            downloaders.ToList().ForEach(d => _downloaders.Add(d.Topic, d));

            _maxDownloaderNumber = maxDownloaderNumber;
        }

        public void StartConsume()
        {
            PullMessage();
        }

        private void PullMessage()
        {
            if (_pulling)
                return;
            _pulling = true;
            var message = _scheduler.Poll();
            _pulling = false;
            Consume(message);
        }

        private async void Consume(RequestMessage message)
        {
            IDownloader downloader;
            _downloaders.TryGetValue(message.Topic, out downloader);
            if (downloader == null)
                return;
            await downloader.DownloadAsync(message.Request);
            PullMessage();
        }
    }
}
