using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Download;
using Plunder.Plugin.Download;

namespace PlunderTestConsole
{ 
    public class FakeDownloader : IDownloader
    {
        private readonly int _maxTaskNumber;
        private int _currentTaskNumber;
        private readonly SemaphoreSlim _ctnLock = new SemaphoreSlim(1);
        public int DownloadingTaskCount
        {
            get { return _currentTaskNumber; }
        }

        public FakeDownloader(int maxTaskNumber)
        {
            _maxTaskNumber = maxTaskNumber;
        }

        public bool IsDefault { get; set; }

        public string Topic => WebPageType.Static;

        public void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloadComplete, Action onConsumed)
        {
            foreach (Request req in requests)
            {
                Interlocked.Increment(ref _currentTaskNumber);
                Task.Run(async () =>
                {
                    try
                    {

                        Console.WriteLine(@"开始执行Http下载，占位符." + req.Url);
                        await Task.Delay(300);
                        Console.WriteLine(@"Http下载完成，占位符" + req.Url);
                        return new Tuple<Request, Response>(new Request(), new Response());
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _currentTaskNumber);
                    }

                }).ContinueWith(t =>
                {
                    //Console.WriteLine(@"执行下载完成事件onDownloadComplete，占位符");
                    onDownloadComplete(t.Result.Item1, t.Result.Item2);
                }).ContinueWith(t => onConsumed);
            }
        }

        public Task DownloadAsync(Request requests, Action<Request, Response> onDownloaded)
        {
            throw new NotImplementedException();
        }

        public bool IsAllowDownload()
        {
            return _maxTaskNumber > _currentTaskNumber;
        }

        public Task<Tuple<Request, Response>> DownloadAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
