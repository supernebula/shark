using System;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Download.Proxy;

namespace Plunder.Download
{
    public interface IDownloader
    {
        Request Request { get;}

        UserAgent UserAgent { get; set; }

        HttpProxy Proxy { get; set; }

        PageType PageType { get; }

        DownloadStatus Status { get; }

        DateTime? DownloadStartTime { get; }

        /// <summary>
        /// millisecond
        /// </summary>
        int HasElapsed { get; }

        Task<Response> DownloadAsync();
    }

}
