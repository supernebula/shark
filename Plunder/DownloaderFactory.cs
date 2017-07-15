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
        private Dictionary<string, Func<IDownloader>> _downloaderThunk = new Dictionary<string, Func<IDownloader>>();
        private ConcurrentDictionary<string, IDownloader> _topicDownloaderDic;
        public DownloaderFactory(IEnumerable<KeyValuePair<string, Func<IDownloader>>> topicDownloaderThunks)
        {
            if (topicDownloaderThunks == null)
                throw new ArgumentNullException(nameof(topicDownloaderThunks));

            _topicDownloaderDic = new ConcurrentDictionary<string, IDownloader>();

            var thunks = topicDownloaderThunks.ToList();
            _downloaderThunk = thunks.ToDictionary(e => e.Key, e => e.Value);
            thunks.ForEach(e => {
                _topicDownloaderDic.TryAdd(e.Key, e.Value.Invoke());
            });
            
        }

        public void Register<TDownloader>(string topic, Func<TDownloader> thunk) where TDownloader : IDownloader
        {
            _topicDownloaderDic.TryAdd(topic, thunk.Invoke());
        }

        public int Count => _downloaderThunk.Values.Count;

        public bool Any()
        {
            return Count > 0;
        }

        //public void Register(string topic, Type downloaderType)
        //{
        //    if (!string.IsNullOrWhiteSpace(topic))
        //        throw new ArgumentNullException(nameof(topic));
        //    if (!downloaderType.IsAssignableFrom(typeof(IDownloader)))
        //        throw new ArgumentException($"{nameof(downloaderType)}不是类型：{typeof(IDownloader).FullName}");

        //    _downloaderCollection.TryAdd(topic, downloaderType);

        public IDownloader Create(string topic)
        {
            IDownloader downloader;
            _topicDownloaderDic.TryGetValue(topic, out downloader);
            return downloader;
        }
    }
}
