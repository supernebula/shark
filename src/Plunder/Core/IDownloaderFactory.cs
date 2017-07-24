
using Plunder.Download;

namespace Plunder.Core
{
    public interface IDownloaderFactory
    {
        IDownloader Create(PageType pageType);

        int Count { get; }

        bool Any();
    }
}
