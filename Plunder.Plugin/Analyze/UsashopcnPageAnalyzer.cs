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

        public async Task<PageResult> AnalyzeAsync(Response response)
        {
            return await ExecuteAsync(response);
        }

        public Task<PageResult> ExecuteAsync(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
