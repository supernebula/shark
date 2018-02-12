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
            site.Id = SiteIndex.DevTest;
            //site.Topic = PageType.Static;
            site.IndexUrl = "http://www.devtest.com/";
            site.Domain = "www.devtest.com";
            site.EnableHttpProxy = false;
            site.Name = "开发模板";
            site.UserAgent = UserAgentCollection.RandomUserAgent().Value;
            site.Charset = "utf-8";
            site.SleepMilliseconds = 2000;
            site.RetryTimes = 1;
            site.AllowedRetryCount = 1;
            site.CycleRetryTimes = 1;
            site.RetrySleepMilliseconds = 10000;
            site.TimeOut = 3000;
            sites.Add(site.Id, site);

            //植物名录
            var site2 = Site.NewDefault;
            site2.Id = SiteIndex.PlantCsdb;
            //site.Topic = PageType.Static;
            site2.IndexUrl = "http://www.plant.csdb.cn/";
            site2.Domain = "www.plant.csdb.cn";
            site2.EnableHttpProxy = false;
            site2.Name = "中国植物主题数据库";
            site2.UserAgent = UserAgentCollection.RandomUserAgent().Value;
            site2.Charset = "utf-8";
            site2.SleepMilliseconds = 200;
            site2.RetryTimes = 1;
            site2.AllowedRetryCount = 1;
            site2.CycleRetryTimes = 1;
            site2.RetrySleepMilliseconds = 10000;
            site2.TimeOut = 3000;
            sites.Add(site2.Id, site2);

            //植物名录
            var site3 = Site.NewDefault;
            site3.Id = SiteIndex.PlantCsdb;
            //site.Topic = PageType.Static;
            site3.IndexUrl = "https://www.lagou.com/";
            site3.Domain = "www.lagou.com";
            site3.EnableHttpProxy = false;
            site3.Name = "拉钩网";
            site3.UserAgent = UserAgentCollection.RandomUserAgent().Value;
            site3.Charset = "utf-8";
            site3.SleepMilliseconds = 200;
            site3.RetryTimes = 1;
            site3.AllowedRetryCount = 1;
            site3.CycleRetryTimes = 1;
            site3.RetrySleepMilliseconds = 10000;
            site3.TimeOut = 3000;
            sites.Add(site2.Id, site2);

            return sites;
        }
    }
}
