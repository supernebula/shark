using Plunder.Download;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder
{
    public class DownloaderFactory
    {
        public DownloaderFactory(Dictionary<string, IDownloader> pageTagDownloaders)
        {
            if (pageTagDownloaders == null)
                throw new ArgumentNullException(nameof(pageTagDownloaders));
            if (!pageTagDownloaders.Any())
                throw new ArgumentOutOfRangeException($"{nameof(pageTagDownloaders)}不包含任何元素");
            _downloaderCollection = new ConcurrentDictionary<string, IDownloader>(pageTagDownloaders.ToList());
        }

        private ConcurrentDictionary<string, IDownloader> _downloaderCollection;

        public IDownloader CreateDownloader(string pageTag)
        {
            IDownloader item = null;
            _downloaderCollection.TryGetValue(pageTag, out item);
            return item;
        }
    }
}
