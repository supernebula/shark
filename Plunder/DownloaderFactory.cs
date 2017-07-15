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

        private ConcurrentDictionary<string, Type> _downloaderCollection;
        public DownloaderFactory()
        {
            _downloaderCollection = new ConcurrentDictionary<string, Type>();
        }

        public void Register<TDownloader>(string topic) where TDownloader : IDownloader
        {
            Register(topic, typeof(TDownloader));
        }

        public void Register(string topic, Type downloaderType)
        {
            if (!string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));
            if (!downloaderType.IsAssignableFrom(typeof(IDownloader)))
                throw new ArgumentException($"{nameof(downloaderType)}不是类型：{typeof(IDownloader).FullName}");

            _downloaderCollection.TryAdd(topic, downloaderType);
        }

        

        public IDownloader CreateDownloader(string topic)
        {
            Type type;
            _downloaderCollection.TryGetValue(topic, out type);

            throw new NotImplementedException();
        }
    }
}
