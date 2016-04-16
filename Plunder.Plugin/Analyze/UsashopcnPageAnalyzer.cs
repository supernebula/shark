
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Plunder.Compoment;
using Plunder.Analyze;
using HtmlAgilityPack;

using Plunder.Plugin.Compoment;
using Site = Plunder.Compoment.Site;

namespace Plunder.Plugin.Analyze
{
    public class UsashopcnPageAnalyzer : IPageAnalyzer
    {
        public static string SiteId => SiteIndex.UsashopcnId;
        public Site Site { get; }

        private readonly IEnumerable<FieldSelector> _fieldXPaths;

        public UsashopcnPageAnalyzer()
        {
            Site = SiteConfiguration.Instance.GetSite(SiteId);
            _fieldXPaths = new Dictionary<string, string> {
                { "Title","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[2]/h2[1]"},
                { "Price","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[2]/span[1]"},
                { "Description","/html[1]/body[1]/div[2]/div[2]/div[1]/div[2]/div[1]/div[2]"},
                { "PicUri","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/img[1]/@src[1]"},
            }.Select(e => new FieldSelector() { FieldName = e.Key, Selector = e.Value });
        }

        public PageResult Analyze(Request request, Response response)
        {
            var site = SiteConfiguration.Instance.GetSite(request.SiteId);
            var doc = new HtmlDocument();
            doc.Load(response.Content);

            var resultFields = XpathSelect(doc, _fieldXPaths);
            var newRequests = FindNewRequest(doc, request, null);
            resultFields.Add(new ResultField() { Name = "Uri", Value = request.Url});
            resultFields.Add(new ResultField() { Name = "SiteName", Value = site.Name });
            resultFields.Add(new ResultField() { Name = "SiteDomain", Value = site.Domain });
            resultFields.Add(new ResultField() { Name = "ElapsedSecond", Value = response.MillisecondTime.ToString() });
            resultFields.Add(new ResultField() { Name = "Downloader", Value = response.Downloader });
            resultFields.Add(new ResultField() { Name = "CommentCount", Value = "0" });
            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = newRequests,
                Channel = Channel.Product,
                Data = resultFields
            };
            return pageResult;
        }

        private IEnumerable<Request> FindNewRequest(HtmlDocument doc, Request request, string newUrlPattern)
        {
            var regex = new Regex(newUrlPattern, RegexOptions.IgnoreCase);
            var newRequests = new List<Request>();
            var dominRegex = new Regex(@"(?<=http://)[\w\.]+[^/]", RegexOptions.IgnoreCase); 

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                var href = link.GetAttributeValue("href", String.Empty);
                if (String.IsNullOrWhiteSpace(href) || !href.StartsWith("http"))
                    continue;
                if (!String.IsNullOrWhiteSpace(newUrlPattern) && !regex.IsMatch(href))
                    continue;
                if(href.Equals(request.Url))
                    continue;
                if(dominRegex.Match(href).Value.Contains(Site.Domain))
                    continue;
                newRequests.Add(new Request() { Url = href, SiteId = request.SiteId, HttpMethod = request.HttpMethod });
            }
            return newRequests;
        }


        private List<ResultField> XpathSelect(HtmlDocument doc, IEnumerable<FieldSelector> selectors)
        {
            var fields = new List<ResultField>().ToList();
            foreach (var selector in selectors)
            {
                if(String.IsNullOrWhiteSpace(selector.Selector))
                    continue;
                var node = doc.DocumentNode.SelectSingleNode(selector.Selector);
                fields.Add(new ResultField { Name = selector.FieldName, Value = node.InnerText.Trim() });
            }
            return fields;
        }
    }
}
