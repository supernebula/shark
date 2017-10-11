using Plunder.Core;
using Plunder.Process.Analyze;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Plunder
{
    public class PageAnalyzerFactory : IPageAnalyzerFactory
    {
        private readonly ConcurrentDictionary<string, Func<IPageAnalyzer>> _analyzerThunkDic;

        public PageAnalyzerFactory()
        {
            _analyzerThunkDic = new ConcurrentDictionary<string, Func<IPageAnalyzer>>();
        }

        private string GenerateKey(string siteId, string topic)
        {
            return $"{siteId}.{topic}";
        }

        public void Register(string siteId, string topic, Func<IPageAnalyzer> analyzerThunk)
        {
            var key = GenerateKey(siteId, topic);
            Func<IPageAnalyzer> delete;
            if (_analyzerThunkDic.ContainsKey(key) && _analyzerThunkDic.TryRemove(key, out delete))
                _analyzerThunkDic.TryAdd(key, analyzerThunk);
            else
                _analyzerThunkDic.TryAdd(key, analyzerThunk);
        }

        public void RegisterByConfig(IEnumerable<string> configFiles)
        {
            throw new NotImplementedException();
        }


        public IPageAnalyzer Create(string siteId, string topic)
        {
            if (string.IsNullOrWhiteSpace(siteId))
                throw new ArgumentNullException(nameof(siteId));

            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentNullException(nameof(topic));
            var key = GenerateKey(siteId, topic);
            Func<IPageAnalyzer> thunk;
            if (!_analyzerThunkDic.TryGetValue(key, out thunk))
                throw new ArgumentException($"不包含参数{nameof(key)}对应的键,{nameof(key)}={key}");
            if(thunk == null)
                throw new NullReferenceException($"与参数{nameof(key)}对应的值为null,{nameof(key)}={key}");
            var item = thunk.Invoke();
            if (item == null)
                throw new NullReferenceException($"与参数{nameof(key)}对应的thunk委托返回null的{typeof(IPageAnalyzer).FullName}");
            return item;
        }

        public int Count => _analyzerThunkDic.Values.Count;

        public bool Any()
        {
            return Count > 0;
        }
    }
}
