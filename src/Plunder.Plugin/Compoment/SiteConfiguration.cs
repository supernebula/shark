using System;
using System.Collections.Generic;
using Plunder.Compoment;
using Plunder.Download;
using Plunder.Plugin.Download;

namespace Plunder.Plugin.Compoment
{
    public class SiteConfiguration
    {
        private readonly Dictionary<string, Site> _sites;

        private static SiteConfiguration _instance;

        public static SiteConfiguration Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SiteConfiguration();
                return _instance;
            }
        }
        public SiteConfiguration()
        {
            _sites = Load();
        }

        public Site GetSite(string id)
        {
            return _sites[id];
        }

        private Dictionary<string, Site> Load()
        {
            var sites = new Dictionary<string, Site>();
            var site = Site.NewDefault;
            site.Id = SiteIndex.Usashopcn;
            site.Topic = WebPageType.Static;
            site.IndexUrl = "http://www.usashopcn.com/";
            site.Domain = "www.usashopcn.com";
            site.IsUseHttpProxy = false;
            site.Name = "美国购物网";
            site.UserAgent = UserAgent.RandomUserAgent().Value;
            site.Charset = "utf-8";
            site.SleepMilliseconds = 2000;
            site.RetryTimes = 1;
            site.AllowedRetryCount = 1;
            site.CycleRetryTimes = 1;
            site.RetrySleepMilliseconds = 10000;
            site.TimeOut = 3000;
            sites.Add(site.Id, site);
            return sites;
        }
    }
}
