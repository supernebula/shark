using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plunder.Process.Analyze;
using Plunder.Compoment;
using Plunder.Plugin.Compoment;
using Plunder.Plugin.Download;

namespace PlunderTestConsole
{
    public class FakePageAnalyzer : IPageAnalyzer
    {
        public string SiteId => SiteIdValue;

        public string PageTag => PageTagValue;
        public Site Site { get; }

        public const string SiteIdValue = SiteIndex.Usashopcn;

        public string PageTagValue = "product.detail";


        public FakePageAnalyzer()
        {
            Site = Plunder.Compoment.Site.NewDefault;
            Site.Id = SiteId;
        }


        public PageResult Analyze(Request request, Response response)
        {
            Console.WriteLine(@"FakePageAnalyzer.Analyze 执行分析");

            request = new Request()
            {
                SiteId = SiteIndex.Usashopcn,
                Url = @"http://www.usashopcn.com/Product/Details/127824",
                HttpMethod = HttpMethod.Get,
                RemainRetryCount = 0
            };

            response = new Response()
            {
                Request = request,
                HttpStatusCode = HttpStatusCode.OK,
                ReasonPhrase = "OK",
                IsSuccessCode = true,
                Content = "测试文本",
                Elapsed = 1000,
                Downloader = "FakeDownloader"
            };

            return new PageResult()
            {
                Topic = WPageType.Static,
                Request = request,
                Response = response
            };
        }
    }
}
