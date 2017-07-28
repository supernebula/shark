
using Plunder.Compoment;
using Plunder.Download;

namespace Plunder.Core
{
    public interface IDownloaderFactory
    {
        IDownloader Create(Request request, PageType pageType);

        int Count { get; }

        bool Any();
    }
}
