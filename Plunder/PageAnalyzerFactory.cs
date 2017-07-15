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
        public PageAnalyzerFactory(Dictionary<string, IPageAnalyzer> pageAnalyzers)
        {
            if (pageAnalyzers == null)
                throw new ArgumentNullException(nameof(pageAnalyzers));
            if (!pageAnalyzers.Any())
                throw new ArgumentOutOfRangeException($"{nameof(pageAnalyzers)}不包含任何元素");
            _pageAnalyzerCollection = new ConcurrentDictionary<string, IPageAnalyzer>(pageAnalyzers.ToList());
        }

        private ConcurrentDictionary<string, Type> _pageAnalyzerCollection;

        public Type CreatePageAnalyzer(string key)
        {
            throw new NotImplementedException();
        }
    }
}
