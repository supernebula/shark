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

        public static HttpClient GetClient(Site site, HttpProxyPool httpProxyPool = null)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer = new CookieContainer() {};
            if(site.IsUseProxy && httpProxyPool != null)
                httpClientHandler.Proxy = httpProxyPool.RandomProxy();
            httpClientHandler.UseProxy = true;
            httpClientHandler.UseCookies = true;
            return new HttpClient(httpClientHandler);
        }
    }
}
