using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Downloader
{
    public interface IDownloader
    {
        string Topic { get; }

        bool IsDefault { get; set; }

        bool IsAllowDownload();

        int DownloadingTaskCount { get;}

        void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> onDownloaded, Action onConsumed);

        Task DownloadAsync(Request requests, Action<Request, Response> onDownloaded);
    }
}
