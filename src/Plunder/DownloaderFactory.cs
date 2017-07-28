using Plunder.Compoment;
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
        private Dictionary<PageType, Func<Request, PageType, IDownloader>> _downloaderThunk = new Dictionary<PageType, Func<Request, PageType, IDownloader>>();
        private ConcurrentDictionary<PageType, Func<Request, PageType, IDownloader>> _topicDownloaderDic;
        public DownloaderFactory(Dictionary<PageType, Func<Request, PageType, IDownloader>> topicDownloaderThunks)
        {
            if (topicDownloaderThunks == null)
                throw new ArgumentNullException(nameof(topicDownloaderThunks));

            _topicDownloaderDic = new ConcurrentDictionary<PageType, Func<Request, PageType, IDownloader>>();

            var thunks = topicDownloaderThunks.ToList();
            _downloaderThunk = thunks.ToDictionary(e => e.Key, e => e.Value);
            thunks.ForEach(e => {
                _topicDownloaderDic.TryAdd(e.Key, e.Value);
            });
            
        }

        public void Register<TDownloader>(PageType pageType, Func<Request, PageType, IDownloader> thunk) where TDownloader : IDownloader
        {
            _topicDownloaderDic.TryAdd(pageType, thunk);
        }

        public int Count => _downloaderThunk.Values.Count;

        public bool Any()
        {
            return Count > 0;
        }


        public IDownloader Create(Request request,PageType pageType)
        {
            Func<Request, PageType, IDownloader> downloaderThunk;
            _topicDownloaderDic.TryGetValue(pageType, out downloaderThunk);
            var downloader = downloaderThunk.Invoke(request, pageType);
            return downloader;
        }
    }
}
