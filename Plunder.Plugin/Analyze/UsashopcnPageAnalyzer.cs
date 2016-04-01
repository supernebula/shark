using Plunder.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Plunder.Compoment;
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
                { "Uri",""},
                { "CommentCount",""},
                { "SiteName",""},
                { "SiteDomain",""},
                { "ElapsedSecond",""},
                { "Downloader",""}
            }.Select(e => new FieldSelector() { FieldName = e.Key, Selector = e.Value });
        }

        public PageResult Analyze(Response response)
        {
            var pageResult = XpathSelect(response, _fieldSelectors, null);
            pageResult.HttpStatCode = response.HttpStatusCode;
            pageResult.Site = Site;
            return pageResult;
        }


        private PageResult XpathSelect(Response response, IEnumerable<FieldSelector> selectors,string newUrlPattern)
        {
            var doc = new HtmlDocument();
            doc.Load(response.Content);

            var fields = new List<ResultField>().ToList();
            foreach (var selector in selectors)
            {
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
            return new PageResult() { Content = response.Content, Result = fields, NewRequests = newRequests };
        }
    }
}
