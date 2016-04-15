using System.Net;
using System.Net.Http;
using Plunder.Proxy;
using Plunder.Compoment;

namespace Plunder.Plugin.Downloader
{
    public static class HttpClientBuilder
    {
        static HttpClientBuilder()
        {

        }

        public static HttpClient GetClient(string siteId, HttpProxyPool httpProxyPool = null)
        {
            var site = SiteConfiguration.Instance.GetSite(siteId);
            var httpClientHandler = new HttpClientHandler {CookieContainer = new CookieContainer() {}};
            if(site.IsUseHttpProxy && httpProxyPool != null)
                httpClientHandler.Proxy = httpProxyPool.RandomProxy();
            httpClientHandler.UseProxy = true;
            httpClientHandler.UseCookies = true;
            return new HttpClient(httpClientHandler);
        }
    }
}
