using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Plunder.Compoment;
using Plunder.Plugin.Compoment;
using Plunder.Process.Analyze;
using HtmlAgilityPack;
using Site = Plunder.Compoment.Site;
using NLog;
namespace Plunder.Plugin.Analyze
{
    public class PlantCsdbListPageAnalyzer : IPageAnalyzer
    {
        private ILogger Logger = LogManager.GetLogger(nameof(PlantCsdbListPageAnalyzer));

        public const string SiteIdValue = SiteIndex.PlantCsdb;

        public const string TargetPageFlagValue = "plantnames.list";

        public Site Site { get; }

        public string SiteId => SiteIdValue;

        public string Topic => "plantnames.list";

        public string TargetPageFlag => TargetPageFlagValue;

        public PlantCsdbListPageAnalyzer()
        {
            Site = SiteConfiguration.Instance.GetSite(SiteId);
        }



        public PageResult Analyze(Response response)
        {
            var request = response.Request;
            var doc = new HtmlDocument();
            if (string.IsNullOrWhiteSpace(response.Content))
                return PageResult.EmptyResponse(/*Site.Topic,*/ request, response, Channel.Product);
            doc.LoadHtml(response.Content);
            var rsultFields = FindList(doc);

            var newRequests = FindNewRequest(doc, request, @"[\s\S]*", "names?page=");

            var group = FindList(doc);

            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = newRequests,
                Topic = Channel.Plant,
                GroupData = group,
                //Topic = Site.Topic
            };
            return pageResult;
        }

        private IEnumerable<IEnumerable<ResultField>>  FindList(HtmlDocument doc)
        {
            var group = new List<IEnumerable<ResultField>>();

            var table = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/div/div[1]/table");

            var tableNode = table.First();
            var trs = tableNode.SelectNodes("tr");
            for (int i = 1; i <= trs.Count; i++)
            {
                var plant = new List<ResultField>();
                var tr = trs[i - 1];
                var latinName = tr.SelectNodes("td[1]").FirstOrDefault()?.InnerText.Trim() ?? string.Empty;
                var namer = tr.SelectNodes("td[2]").FirstOrDefault()?.InnerText.Trim() ?? string.Empty;
                var zhName = tr.SelectNodes("td[3]").FirstOrDefault()?.InnerText.Trim() ?? string.Empty;
                var locality = tr.SelectNodes("td[4]").FirstOrDefault()?.InnerText.Trim() ?? string.Empty;

                plant.Add(new ResultField { Name = "LatinName", Value = latinName });
                plant.Add(new ResultField { Name = "Namer", Value = namer });
                plant.Add(new ResultField { Name = "ZhName", Value = zhName });
                plant.Add(new ResultField { Name = "Locality", Value = locality });
                var listUrl = "http://www.plant.csdb.cn/taxonpage?sname=" + latinName.Replace(" ", "%20");
                plant.Add(new ResultField { Name = "ListUrl", Value = listUrl });
                group.Add(plant);
            }

            return group;
        }

        private IEnumerable<Request> FindNewRequest(HtmlDocument doc, Request request, string newUrlPattern, string extractUrlPattern)
        {
            if (String.IsNullOrWhiteSpace(extractUrlPattern))
                return null;
            var newRegex = new Regex(newUrlPattern, RegexOptions.IgnoreCase);
            //var extractRegex = new Regex(extractUrlPattern, RegexOptions.IgnoreCase);
            var newRequests = new List<Request>();
            var dominRegex = new Regex(@"(?<=http://)[\w\.]+[^/]", RegexOptions.IgnoreCase);

            var linkNodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes == null)
                return newRequests;

            foreach (HtmlNode link in linkNodes)
            {
                var href = link.GetAttributeValue("href", String.Empty).Trim();
                href = AbsoluteUrl(href);
                if (request.Url.Equals(href.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    continue;
                if (String.IsNullOrWhiteSpace(href) || !href.StartsWith("http"))
                    continue;
                if (!String.IsNullOrWhiteSpace(newUrlPattern) && !newRegex.IsMatch(href))
                    continue;
                if (href.Equals(request.Url))
                    continue;
                if (!dominRegex.Match(href).Value.Contains(Site.Domain))
                    continue;
                //var urlTye = extractRegex.IsMatch(href) ? UrlType.Extracting : UrlType.Navigation;
                if (href.IndexOf("names?page=", StringComparison.CurrentCultureIgnoreCase) < 0 && href.IndexOf("names?page=", StringComparison.CurrentCultureIgnoreCase) < 0)
                    continue;

                var urlTye = UrlType.Navigation;
                //if (href.IndexOf("/Product/Details/", StringComparison.CurrentCultureIgnoreCase) != -1)
                //    urlTye = UrlType.Target;
                newRequests.Add(new Request(href) { UrlType = urlTye, SiteId = request.SiteId, HttpMethod = request.HttpMethod, PageType = request.PageType, Topic = TargetPageFlagValue });
            }
            return newRequests;
        }

        private string AbsoluteUrl(string url)
        {
            if (url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
                return url;
            return (new Uri(new Uri(String.Format("http://{0}/", Site.Domain)), url)).AbsoluteUri;
        }

    }
}
