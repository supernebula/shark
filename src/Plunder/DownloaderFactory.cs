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
        private Dictionary<PageType, Func<IDownloader>> _downloaderThunk = new Dictionary<PageType, Func<IDownloader>>();
        private ConcurrentDictionary<PageType, IDownloader> _topicDownloaderDic;
        public DownloaderFactory(IEnumerable<KeyValuePair<PageType, Func<IDownloader>>> topicDownloaderThunks)
        {
            if (topicDownloaderThunks == null)
                throw new ArgumentNullException(nameof(topicDownloaderThunks));

            _topicDownloaderDic = new ConcurrentDictionary<PageType, IDownloader>();

            var thunks = topicDownloaderThunks.ToList();
            _downloaderThunk = thunks.ToDictionary(e => e.Key, e => e.Value);
            thunks.ForEach(e => {
                _topicDownloaderDic.TryAdd(e.Key, e.Value.Invoke());
            });
            
        }

        public void Register<TDownloader>(PageType pageType, Func<TDownloader> thunk) where TDownloader : IDownloader
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

        public IDownloader Create(PageType pageType)
        {
            IDownloader downloader;
            _topicDownloaderDic.TryGetValue(pageType, out downloader);
            return downloader;
        }
    }
}
