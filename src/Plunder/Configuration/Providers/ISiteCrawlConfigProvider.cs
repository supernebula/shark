using Plunder.Process.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Configuration.Providers
{
    interface ISiteCrawlConfigProvider
    {
        IEnumerable<SiteCrawlConfig> All();

        SiteCrawlConfig Get(string SiteId, string topic);
    }
}
