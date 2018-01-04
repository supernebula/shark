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
    public class PlantCsdbPhotoPageAnalyzer : IPageAnalyzer
    {
        private ILogger Logger = LogManager.GetLogger(nameof(PlantCsdbListPageAnalyzer));

        public const string SiteIdValue = SiteIndex.PlantCsdb;

        public const string TargetPageFlagValue = "plantname.detail";

        public Site Site { get; }

        public string SiteId => SiteIdValue;

        public string Topic => "plantname.detail";

        public string TargetPageFlag => TargetPageFlagValue;

        private readonly FieldSelector latinSelector;

        private readonly FieldSelector thumbImgUrlSelector;

        public PlantCsdbPhotoPageAnalyzer()
        {
            Site = SiteConfiguration.Instance.GetSite(SiteId);
            latinSelector = new FieldSelector() { FieldName = "LatinName", Selector = "//*[@id=\"content\"]/h1" };
            thumbImgUrlSelector = new FieldSelector() { FieldName = "ThumbImgUrl", Selector = "//*[@id=\"quicktabs_tabpage__fourth\"]/a/img" };
        }



        public PageResult Analyze(Response response)
        {
            var request = response.Request;
            var doc = new HtmlDocument();
            if (string.IsNullOrWhiteSpace(response.Content))
                return PageResult.EmptyResponse(/*Site.Topic,*/ request, response, Topic);
            doc.LoadHtml(response.Content);

            var resultGroup = new List<IEnumerable<ResultField>>();
            if (request.UrlType == UrlType.Target)
            {
                var imgFields = XpathSelectImage(doc, thumbImgUrlSelector);
                if (imgFields != null)
                {
                    foreach (var item in imgFields)
                    {
                        var resultFields = new List<ResultField>();
                        var latinField = XpathSelectLatinName(doc, latinSelector);
                        var thumbUrl = item.Value;
                        var normalUrl = thumbUrl.Replace("Thumbnail", "Normal");
                        var thumbPath = (new Uri(thumbUrl)).AbsolutePath;
                        var normalPath = (new Uri(normalUrl)).AbsolutePath;

                        resultFields.Add(new ResultField() { Name = "LatinName", Value = latinField?.Value ?? string.Empty });
                        resultFields.Add(new ResultField() { Name = "ThumbImgUrl", Value = thumbUrl });
                        resultFields.Add(new ResultField() { Name = "NormalImgUrl", Value = normalUrl });
                        resultFields.Add(new ResultField() { Name = "SourceSite", Value = Site.Domain });
                        resultFields.Add(new ResultField() { Name = "ThumbPath", Value = thumbPath });
                        resultFields.Add(new ResultField() { Name = "NormalPath", Value = normalPath });

                        resultGroup.Add(resultFields);
                    }
                }
            }

            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = null,
                Topic = Topic,
                GroupData = resultGroup
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
