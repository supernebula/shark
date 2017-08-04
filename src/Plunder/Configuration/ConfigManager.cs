using Plunder.Compoment;
using Plunder.Compoment.Models;
using Plunder.Compoment.Values;
using Plunder.Configuration.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plunder.Configuration
{
    public class ConfigManager
    {
        public ICookieProvider CookieProvider { get; private set; }

        public IExtractRuleProvider ExtractRuleProvider { get; private set; }

        public IHttpProxyProvider HttpProxyProvider { get; private set; }

        public IUserAgentProvider UserAgentProvider { get; private set; }

        public ConfigManager(ICookieProvider cookieProvider, IExtractRuleProvider ruleProvider, IHttpProxyProvider httpProxyProvider, IUserAgentProvider userAgentProvider)
        {
            CookieProvider = cookieProvider ?? throw new ArgumentNullException(nameof(cookieProvider));
            ExtractRuleProvider = ruleProvider ?? throw new ArgumentNullException(nameof(ruleProvider));
            HttpProxyProvider = httpProxyProvider ?? throw new ArgumentNullException(nameof(httpProxyProvider));
            UserAgentProvider = userAgentProvider ?? throw new ArgumentNullException(nameof(userAgentProvider));
        }

        public Cookie GetAnyCookie(Guid siteId)
        {
            var cookies = CookieProvider.GetAll(siteId);
            var item = RandomItem(cookies);
            return item;
        }

        public ExtractRule GetExtractRule(Guid siteId, string url, string htmlContent = null)
        {
            var rules = ExtractRuleProvider.GetAll(siteId);
            var item = rules.FirstOrDefault(e => e.IsMatch(url, htmlContent));
            return item;
        }

        public HttpProxy GetHttpProxy(HttpProxyType defaultType = HttpProxyType.HighAnonymity)
        {
            var proxies = HttpProxyProvider.GetAll(defaultType);
            var item = RandomItem(proxies);
            return item;
        }

        public UserAgent GetUserAgent(DeviceType defaultType = DeviceType.PC)
        {
            var agents = UserAgentProvider.GetAll(defaultType);
            var item = RandomItem(agents);
            return item;
        }

        private T RandomItem<T>(IEnumerable<T> collection)
        {
            if (!collection.Any())
                return default(T);
            if (collection.Count() == 1)
                return collection.First();
            var index = (new Random(Guid.NewGuid().GetHashCode())).Next(0, collection.Count() - 1);
            var item = collection.ElementAt(index);
            return item;
        }

    }
}
