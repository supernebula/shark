using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Download
{

    public enum DownloadStatus
    {
        NotStarted = 0,

        Downloading = 1,

        Successful = 2,

        Failed = 3
    }
}
