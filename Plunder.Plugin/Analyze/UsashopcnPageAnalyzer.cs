using Plunder.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Plugin.Analyze
{
    public class UsashopcnPageAnalyzer : IPageAnalyzer 
    {
        public Guid Id { get; set; }

        public Site Site { get; set; }

        public async Task<PageResult> AnalyzeAsync(Response response)
        {
            await Task.Run(() =>
            {
                return null;

            });
        }

        public Task<PageResult> Xpath(string html, IEnumerable<FieldSelector> xpathSelector)
        {
            throw new NotImplementedException();
        }
    }

    public class FieldSelector
    {
        public string Name { get; set; }

        public string Selector { get; set; }
    }
}
