using System.Data;
using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public interface IDownloader : IConsumer
    {
        Site Site { get; set; }

        void Init(IMessage<Request> requestMessage, HttpProxy proxy);

        Task<bool> DownloadAsync();

        void SetProxy(HttpProxy proxy);


        void Reset();


    }
}
