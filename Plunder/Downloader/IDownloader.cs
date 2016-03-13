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

        Task<string> DownloadAsync(Request request);
    }
}
