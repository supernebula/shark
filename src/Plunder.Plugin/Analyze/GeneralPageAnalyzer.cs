using Plunder.Process.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Plugin.Analyze
{
    public class GeneralPageAnalyzer : IPageAnalyzer
    {
        public Site Site => throw new NotImplementedException();

        public string SiteId => throw new NotImplementedException();

        public string TargetPageFlag => throw new NotImplementedException();

        public PageResult Analyze(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
