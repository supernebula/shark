using Plunder.Process.Analyze;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder
{


    public class PageAnalyzerFactory
    {
        private ConcurrentDictionary<string, Func<IPageAnalyzer>> _analyzerThunkDic;

        public PageAnalyzerFactory()
        {
            //if (pageAnalyzers == null)
            //    throw new ArgumentNullException(nameof(pageAnalyzers));
            //if (!pageAnalyzers.Any())
            //    throw new ArgumentOutOfRangeException($"{nameof(pageAnalyzers)}不包含任何元素");
            _analyzerThunkDic = new ConcurrentDictionary<string, Func<IPageAnalyzer>>();
        }

        private string GenerateKey(string siteId, string pageTag)
        {
            return $"{siteId}.{pageTag}";
        }

        public void Register(string siteId, string pageTag, Func<IPageAnalyzer> analyzerThunk)
        {
            var key = GenerateKey(siteId, pageTag);
            Func<IPageAnalyzer> delete;
            if (_analyzerThunkDic.ContainsKey(key) && _analyzerThunkDic.TryRemove(key, out delete))
                _analyzerThunkDic.TryAdd(key, analyzerThunk);
            else
                _analyzerThunkDic.TryAdd(key, analyzerThunk);
        }




        public Type CreatePageAnalyzer(string key)
        {
            throw new NotImplementedException();
        }
    }
}
