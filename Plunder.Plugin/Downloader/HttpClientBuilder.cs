using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Downloader
{
    public static class HttpClientBuilder
    {
        static HttpClientBuilder()
        {

        }

        public static HttpClient GetClient(Site site)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.CookieContainer = new CookieContainer() {};
            httpClientHandler.Proxy = site.HttpProxyPool.RandomProxy();
            httpClientHandler.UseProxy = true;
            httpClientHandler.UseCookies = true;
            return new HttpClient(httpClientHandler);
        }
    }
}
