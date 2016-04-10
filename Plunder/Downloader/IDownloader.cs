using Plunder.Compoment;
using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public interface IDownloader
    {
        string Topic { get; }

        bool IsAllowDownload();

        int DownloadingTaskCount { get; }

        void DownloadAsync(IEnumerable<Request> requests, Action<Request, Response> singleContinueWith);

        Task<Response> DownloadAsync(Request request);
    }
}
