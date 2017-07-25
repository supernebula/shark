using System.Net;
using System.Net.Http;
using Plunder.Download.Proxy;
using Plunder.Plugin.Compoment;
using Plunder.Compoment;

namespace Plunder.Plugin.Download
{
    public static class HttpClientBuilder
    {
        static HttpClientBuilder()
        {

        }

        public static HttpClient GetClient(Request request)
        {
            var site = SiteConfiguration.Instance.GetSite(request.SiteId);
            var httpClientHandler = new HttpClientHandler {CookieContainer = new CookieContainer() {}};
            if(site != null && site.EnableHttpProxy)
                httpClientHandler.Proxy = HttpProxyPool.RandomProxy();
            httpClientHandler.UseProxy = true;
            httpClientHandler.UseCookies = true;
            return new HttpClient(httpClientHandler);
        }
    }
}
