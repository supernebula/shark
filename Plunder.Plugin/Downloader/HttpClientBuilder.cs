using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plunder.Proxy;

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
