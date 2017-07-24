
using Plunder.Download;

namespace Plunder.Core
{
    public interface IDownloaderFactory
    {
        IDownloaderOld Create(PageType pageType);

        int Count { get; }

        bool Any();
    }
}
