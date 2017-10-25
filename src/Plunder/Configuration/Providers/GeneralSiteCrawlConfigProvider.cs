using System;
using System.Collections.Generic;
using System.Linq;
using Plunder.Process.Analyze;
using System.IO;
using Evol.Utilities.Serialization;

namespace Plunder.Configuration.Providers
{
    public class GeneralSiteCrawlConfigProvider : ISiteCrawlConfigProvider
    {
        private string xmlConfigFolder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crawlConfig");

        public GeneralSiteCrawlConfigProvider()
        {

        }

        public IEnumerable<SiteCrawlConfig> All()
        {
            var configs = new List<SiteCrawlConfig>();
            var files = Directory.GetFiles(xmlConfigFolder);
            var configFiles = files.Where(e => Path.GetExtension(e).Equals(".xml")).ToList();
            if (!configFiles.Any())
                return configs;
            foreach (var path in configFiles)
            {
                var str = File.ReadAllText(path);
                var config = JsonUtility.Deserialize<SiteCrawlConfig>(str);
                configs.Add(config);
            }

            return configs;
        }

        public SiteCrawlConfig Get(string siteId, string topic)
        {
            var all = All();
            var item = all.FirstOrDefault(e => e.SiteId == siteId && e.Topic == topic);
            return item;
        }
    }
}
