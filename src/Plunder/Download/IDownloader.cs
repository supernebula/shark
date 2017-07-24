using Plunder.Compoment;
using Plunder.Download.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Download
{
    public interface IDownloader
    {
        Request Request { get; set; }

        UserAgent UserAgent { get; set; }

        HttpProxy Proxy { get; set; }

        PageType PageType { get; }

        DownloadStatus Status { get; }

        DateTime? StartDownloadTime { get; }

        Task<Response> DownloadAsync();
    }

}
