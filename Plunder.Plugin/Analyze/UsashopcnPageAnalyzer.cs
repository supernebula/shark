using Plunder.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Plugin.Analyze
{
    public class UsashopcnPageAnalyzer : IPageAnalyzer 
    {
        public Guid Id { get; set; }

        public Site Site { get; set; }

        public PageResult Analyze(Response response)
        {
            var pageResult = XpathSelect(response.Content, null, null);
            pageResult.HttpStatCode = response.HttpStatusCode;
            pageResult.Site = Site;
            return pageResult;
        }

        private PageResult XpathSelect(string html, IEnumerable<FieldSelector> selectors,string newUrlRegex)
        {
            var fields = new List<ResultField>();
            foreach (var selector in selectors)
            {
                // fields.Add(func(html, selector));
            }

            var newRequests = new List<Request>();
            //var urls = $(html).Select("a");
            //foreach (var url in urls)
            //{
            //    if (Regex.IsMatch(newUrlRegex))
            //        newUrls.Add(url);
            //newRequests.Add(new Request() { Uri = url, Site = Site, Method = "GET" });

            //}

            return new PageResult() { Content = html, Result = fields, NewRequests = newRequests };
        }

    }



}
