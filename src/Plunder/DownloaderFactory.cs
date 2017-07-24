using Plunder.Core;
using Plunder.Download;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Plunder
{
    public class DownloaderFactory : IDownloaderFactory
    {
        private Dictionary<PageType, Func<IDownloaderOld>> _downloaderThunk = new Dictionary<PageType, Func<IDownloaderOld>>();
        private ConcurrentDictionary<PageType, IDownloaderOld> _topicDownloaderDic;
        public DownloaderFactory(IEnumerable<KeyValuePair<PageType, Func<IDownloaderOld>>> topicDownloaderThunks)
        {
            if (topicDownloaderThunks == null)
                throw new ArgumentNullException(nameof(topicDownloaderThunks));

            _topicDownloaderDic = new ConcurrentDictionary<PageType, IDownloaderOld>();

            var thunks = topicDownloaderThunks.ToList();
            _downloaderThunk = thunks.ToDictionary(e => e.Key, e => e.Value);
            thunks.ForEach(e => {
                _topicDownloaderDic.TryAdd(e.Key, e.Value.Invoke());
            });
            
        }

        public void Register<TDownloader>(PageType pageType, Func<TDownloader> thunk) where TDownloader : IDownloaderOld
        {
            _topicDownloaderDic.TryAdd(pageType, thunk.Invoke());
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

        public IDownloaderOld Create(PageType pageType)
        {
            IDownloaderOld downloader;
            _topicDownloaderDic.TryGetValue(pageType, out downloader);
            return downloader;
        }
    }
}
