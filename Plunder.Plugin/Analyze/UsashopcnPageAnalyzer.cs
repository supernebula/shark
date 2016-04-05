
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Plunder.Compoment;
using Plunder.Analyze;
using HtmlAgilityPack;

namespace Plunder.Plugin.Analyze
{
    public class UsashopcnPageAnalyzer : IPageAnalyzer 
    {
        public Guid Id { get; set; }

        public Site Site { get; set; }

        private IEnumerable<FieldSelector> _fieldSelectors;

        public UsashopcnPageAnalyzer()
        {
            _fieldSelectors = new Dictionary<string, string> {
                { "Title","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[2]/h2[1]"},
                { "Price","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[2]/span[1]"},
                { "Description","/html[1]/body[1]/div[2]/div[2]/div[1]/div[2]/div[1]/div[2]"},
                { "PicUri","/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/img[1]/@src[1]"},
            }.Select(e => new FieldSelector() { FieldName = e.Key, Selector = e.Value });
        }

        public PageResult Analyze(Request request, Response response)
        {
            var resultFields = XpathSelect(response, _fieldSelectors, null);
            resultFields.Add(new ResultField() { Name = "Uri", Value = request.Uri});
            resultFields.Add(new ResultField() { Name = "SiteName", Value = request.Site.Name });
            resultFields.Add(new ResultField() { Name = "SiteDomain", Value = request.Site.Domain });
            resultFields.Add(new ResultField() { Name = "ElapsedSecond", Value = response.MillisecondTime.ToString() });
            resultFields.Add(new ResultField() { Name = "Downloader", Value = request.Downloader });
            resultFields.Add(new ResultField() { Name = "CommentCount", Value = "0" });
            var pageResult = new PageResult
            {
                Result = resultFields,
                HttpStatCode = response.HttpStatusCode,
                Site = request.Site
            };

            return pageResult;
        }


        private List<ResultField> XpathSelect(Response response, IEnumerable<FieldSelector> selectors,string newUrlPattern)
        {
            var doc = new HtmlDocument();
            doc.Load(response.Content);

            var fields = new List<ResultField>().ToList();
            foreach (var selector in selectors)
            {
                if(String.IsNullOrWhiteSpace(selector.Selector))
                    continue;
                var node = doc.DocumentNode.SelectSingleNode(selector.Selector);
                fields.Add(new ResultField { Name = selector.FieldName, Value = node.InnerText.Trim() });
            }

            var newRequests = new List<Request>();
            var hrefs = doc.DocumentNode.Descendants("a").ToList().Select(a => a.GetAttributeValue("href", String.Empty).Trim());

            var regex = new Regex(newUrlPattern, RegexOptions.IgnoreCase);
            foreach (string href in hrefs)
            {
                if(String.IsNullOrWhiteSpace(href) || !href.StartsWith("http"))
                    continue;
                if(!String.IsNullOrWhiteSpace(newUrlPattern) && !regex.IsMatch(href))
                    continue;
                newRequests.Add(new Request() { Uri = href, Site = Site, Method = "GET" });
            }
            return fields;
        }
    }
}
