using Plunder.Compoment;
using Plunder.Proxy;
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
            //httpClientHandler.CookieContainer.Add(new Cookie());
            if (site.IsUseHttpProxy)
            {
                httpClientHandler.UseProxy = true;
                httpClientHandler.Proxy = HttpProxyPool.Instance.RandomProxy();
            }
            
            httpClientHandler.UseCookies = true;
            var httpClient = new HttpClient(httpClientHandler);
            return httpClient;
        }
    }
}
