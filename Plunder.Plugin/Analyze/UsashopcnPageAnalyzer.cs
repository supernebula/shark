using Plunder.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using HtmlAgilityPack;

namespace Plunder.Plugin.Analyze
{
    public class UsashopcnPageAnalyzer : IPageAnalyzer 
    {
        public Guid Id { get; set; }

        public Site Site { get; set; }

        public PageResult Analyze(Response response)
        {
            var pageResult = XpathSelect(response, null, null);
            pageResult.HttpStatCode = response.HttpStatusCode;
            pageResult.Site = Site;
            return pageResult;
        }


        private PageResult XpathSelect(Response response, IEnumerable<FieldSelector> selectors,string newUrlRegex)
        {

            var doc = new HtmlDocument();
            doc.Load(response.Content);

            var fields = new List<ResultField>().ToList();
            foreach (var selector in selectors)
            {
                var node = doc.DocumentNode.SelectSingleNode(selector.XpathSelector);
                fields.Add(new ResultField { Name = selector.FieldName, Value = node.InnerText.Trim() });
            }

            var newRequests = new List<Request>();
            var links = doc.DocumentNode.Descendants("a").ToList();
            links.ForEach((n) =>
            {
                var href = n.GetAttributeValue("href", String.Empty);
                if (href.Trim().StartsWith("http")) // && Regex.IsMatch(newUrlRegex)
                    newRequests.Add(new Request() { Uri = href, Site = Site, Method = "GET" });

            });
           

            return new PageResult() { Content = response.Content, Result = fields, NewRequests = newRequests };
        }

    }



}
