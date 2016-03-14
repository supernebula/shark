//using System;
//using Plunder.Proxy;
//using Plunder.Compoment;
//using Plunder.Scheduler;
//using System.Threading.Tasks;

//namespace Plunder.Downloader
//{
//    [Obsolete]
//    public interface IDownloaderBak : IConsumerBak
//    {
//        Site Site { get; set; }


//        void Init(Guid id, IMessage<Request> requestMessage, HttpProxy proxy);

//        Task<string> DownloadAsync();

//        void SetProxy(HttpProxy proxy);


//        void Reset();


//    }
//}
