using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Downloader
{
    public interface IDownloader
    {
        
        void Download(Request request);
    }
}
