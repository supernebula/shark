using Plunder.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public class HttpSimpleDownloaderPool : ICustomerPool<IDownloader>
    {
        public IDownloader Take()
        {
            throw new NotImplementedException();
        }
    }
}
