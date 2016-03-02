using Plunder.Proxy;
using Plunder.Compoment;
using Plunder.Scheduler;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public interface IDownloader : IConsumer
    {
        void Init(IMessage<Request> requestMessage, HttpProxy proxy);

        Task<bool> Download();
    }
}
