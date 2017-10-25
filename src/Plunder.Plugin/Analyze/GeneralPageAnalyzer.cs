using Plunder.Process.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Plunder.Compoment;
using Plunder.Configuration;
using System.Text.RegularExpressions;

namespace Plunder.Plugin.Analyze
{
    public class GeneralPageAnalyzer : IPageAnalyzer
    {
        public Site Site { get; private set; }

        public string SiteId { get; private set; }

        public string Topic { get; private set; }

        private SiteCrawlConfig crawlConfig;

        private readonly List<FieldSelector> fieldXPaths;

        public string TargetPageFlag => throw new NotImplementedException();

        public GeneralPageAnalyzer(Site site, SiteCrawlConfig crawlConf)
        {
            crawlConfig = crawlConf;
            SiteId = crawlConf.SiteId;
            Topic = crawlConf.Topic;

            var fileMatchs = new Dictionary<string, string>();

            var fieldXPaths = new List<FieldSelector>();
            var tagetPage = crawlConfig.Targets.FirstOrDefault();
            foreach (var fieldRule in tagetPage.FieldRules)
            {
                fieldXPaths.Add(new FieldSelector { FieldName = fieldRule.Name, Selector = fieldRule.XPath });
            }
        }

        public PageResult Analyze(Response response)
        {
            var request = response.Request;

            if (string.IsNullOrWhiteSpace(response.Content))
                return PageResult.EmptyResponse(/*Site.Topic,*/ request, response, request.Topic);
            var doc = new HtmlDocument();
            doc.LoadHtml(response.Content);
            var newRequests = FindNewRequest(doc, request, @"[\s\S]*", "/Product/Details/");//todo: regexPattern
            List<ResultField> resultFields = null;
            if (request.UrlType == UrlType.Target)
            {
                resultFields = XpathSelect(doc, fieldXPaths);

                resultFields.Add(new ResultField() { Name = "SiteId", Value = request.Url });
                resultFields.Add(new ResultField() { Name = "SiteName", Value = request.Url });
                resultFields.Add(new ResultField() { Name = "Domain", Value = request.Url });
                resultFields.Add(new ResultField() { Name = "Topic", Value = request.Url });
                resultFields.Add(new ResultField() { Name = "Url", Value = request.Url });
                resultFields.Add(new ResultField() { Name = "ElapsedMsec", Value = response.Elapsed.ToString() });
                resultFields.Add(new ResultField() { Name = "Downloader", Value = response.DownloaderType.FullName });
            }

            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = newRequests,
                Topic = Topic,
                Data = resultFields
            };
            return pageResult;
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
                if (href.IndexOf("/Product/Details/", StringComparison.CurrentCultureIgnoreCase) < 0 && href.IndexOf("/Product/List/", StringComparison.CurrentCultureIgnoreCase) < 0)
                    continue;

                var urlTye = UrlType.Navigation;
                if (href.IndexOf("/Product/Details/", StringComparison.CurrentCultureIgnoreCase) != -1)
                    urlTye = UrlType.Target;
                newRequests.Add(new Request(href) { UrlType = urlTye, SiteId = request.SiteId, HttpMethod = request.HttpMethod, PageType = request.PageType, Topic = Topic });
            }
            return newRequests;
        }

        private string AbsoluteUrl(string url)
        {
            if (url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
                return url;
            return (new Uri(new Uri(String.Format("http://{0}/", Site.Domain)), url)).AbsoluteUri;
        }

        private List<ResultField> XpathSelect(HtmlDocument doc, IEnumerable<FieldSelector> selectors)
        {
            var fields = new List<ResultField>().ToList();
            foreach (var selector in selectors)
            {
                if (String.IsNullOrWhiteSpace(selector.Selector))
                    continue;
                var node = doc.DocumentNode.SelectSingleNode(selector.Selector);
                if (node.Name == "img")
                {
                    var src = node.Attributes["src"] != null ? node.Attributes["src"].Value : string.Empty;
                    fields.Add(new ResultField { Name = selector.FieldName, Value = src });
                    continue;
                }

                fields.Add(new ResultField { Name = selector.FieldName, Value = node == null ? null : node.InnerText.Trim() });
            }
            return fields;
        }

    }
}
