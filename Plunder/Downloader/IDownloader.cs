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


        //Task DownloadAsync(Request requests, Action<Request, Response> onDownloaded);

        Task<Tuple<Request, Response>> DownloadAsync(Request request);
    }
}
