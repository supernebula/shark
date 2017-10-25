using Plunder.Configuration.Providers;
using Plunder.Ioc;
using Plunder.Process.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plunder.Configuration
{
    public class CrawlConfigManager
    {
        private IIocManager iocManager;

        public CrawlConfigManager(IIocManager ioc)
        {
            iocManager = ioc;
        }

        public SiteCrawlConfig GetConfig(string siteId, string topic)
        {
            var provider = iocManager.GetService<ISiteCrawlConfigProvider>();
            var config = provider.Get(siteId, topic);
            return config;
        }

        public List<SiteCrawlConfig> All()
        {
            var provider = iocManager.GetService<ISiteCrawlConfigProvider>();
            var configs = provider.All().ToList();
            return configs;
        }
    }
}
