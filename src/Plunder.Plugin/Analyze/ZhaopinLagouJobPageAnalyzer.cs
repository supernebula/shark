using System;
using System.Collections.Generic;
using System.Linq;
using Plunder.Compoment;
using Plunder.Plugin.Compoment;
using Plunder.Process.Analyze;
using HtmlAgilityPack;
using Site = Plunder.Compoment.Site;
using NLog;
namespace Plunder.Plugin.Analyze
{
    public class ZhaopinLagouJobPageAnalyzer : IPageAnalyzer
    {
        private ILogger Logger = LogManager.GetLogger(nameof(ZhaopinLagouJobPageAnalyzer));

        public const string SiteIdValue = SiteIndex.PlantCsdb;

        public const string TargetPageFlagValue = "lagou.detail";

        public Site Site { get; }

        public string SiteId => SiteIdValue;

        public string Topic => "lagou.detail";

        public string TargetPageFlag => TargetPageFlagValue;


        private readonly List<FieldSelector> _fieldSelectors;

        public ZhaopinLagouJobPageAnalyzer()
        {
            Site = SiteConfiguration.Instance.GetSite(SiteId);
            _fieldSelectors = new List<FieldSelector>();
            _fieldSelectors.Add(new FieldSelector() { FieldName = "JobName", Selector = "/html/body/div[2]/div/div[1]/div/span" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Salary", Selector = "/html/body/div[2]/div/div[1]/dd/p[1]/span[1]" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_detail\"]/dd[1]/p" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_detail\"]/dd[2]/div" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_detail\"]/dd[3]/div[1]" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_company\"]/dd/ul/li[3]" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_company\"]/dd/ul/li[4]/a[@href]" });
            _fieldSelectors.Add(new FieldSelector() { FieldName = "Lightspot", Selector = "//*[@id=\"job_detail\"]/dd[1]/p" });
        }



        public PageResult Analyze(Response response)
        {
            var request = response.Request;
            var doc = new HtmlDocument();
            if (string.IsNullOrWhiteSpace(response.Content))
                return PageResult.EmptyResponse(/*Site.Topic,*/ request, response, Topic);
            doc.LoadHtml(response.Content);
            var result = new List<ResultField>();
            foreach (var selector in _fieldSelectors)
            {
                var resultField = XpathSelectLatinName(doc, selector);
                result.Add(resultField);
            }

            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = null,
                Topic = Topic,
                Data = result
            };
            return pageResult;
        }

        private ResultField XpathSelectLatinName(HtmlDocument doc, FieldSelector latinSelector)
        {
            if (String.IsNullOrWhiteSpace(latinSelector.Selector))
                return null;
            var node = doc.DocumentNode.SelectSingleNode(latinSelector.Selector);
            var field = new ResultField { Name = latinSelector.Selector, Value = node == null ? null : node.InnerText.Trim() };
            return field;
        }


        private List<ResultField> XpathSelectImage(HtmlDocument doc, FieldSelector imgSelector)
        {
            var fields = new List<ResultField>().ToList();
            if (String.IsNullOrWhiteSpace(imgSelector.Selector))
                return null;
            var nodes = doc.DocumentNode.SelectNodes(imgSelector.Selector);
            if (nodes == null)
                return null;
            foreach (var node in nodes)
            {
                if (node.Name == "img")
                {
                    var src = node.Attributes["src"] != null ? node.Attributes["src"].Value : string.Empty;
                    fields.Add(new ResultField { Name = imgSelector.FieldName, Value = src });
                    continue;
                }

                fields.Add(new ResultField { Name = imgSelector.FieldName, Value = node == null ? null : node.InnerText.Trim() });
            }



            return fields;
        }
    }
}
