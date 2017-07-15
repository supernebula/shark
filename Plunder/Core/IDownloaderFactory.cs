
using Plunder.Download;

namespace Plunder.Core
{
    public interface IDownloaderFactory
    {
        IDownloader Create(string topic);

        int Count { get; }

        bool Any();
    }
}
