using System;
using System.Collections.Generic;
using Plunder.Compoment;
using Plunder.Plugin.Compoment;
using Plunder.Process.Analyze;
using HtmlAgilityPack;
using Site = Plunder.Compoment.Site;
using NLog;
namespace Plunder.Plugin.Analyze
{
    #region 拉勾网 职位搜索分页结果
    public class LagouListAjaxResult
    {
        public bool Success { get; set; }

        public LagouListAjaxContent Content { get; set; }
    }

    public class LagouListAjaxContent
    {
        public LagouListAjaxPositionResult PositionResult { get; set; }
    }

    public class LagouListAjaxPositionResult
    {
        public int TotalCount { get; set; }

        public List<JobInfo> JobResult { get; set; }

    }

    public class JobInfo
    {
        public string PositionId { get; set; }

        public string PositionName { get; set; }

        public string WorkYear { get; set; }

        public string Education { get; set; }

        public string FinanceStage { get; set; }

        public string CompanyLogo { get; set; }

        public string IndustryField { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Salary { get; set; }

        public string PositionAdvantage { get; set; }

        public string CompanyShortName { get; set; }



        public string CreateTime { get; set; }

        public string[] PositionLables { get; set; }

        public string CompanySize { get; set; }

        public string FormatCreateTime { get; set; }

        public string FirstType { get; set; }

        public string SecondType { get; set; }

    }

    #endregion

    public class ZhaopinLagouListPageAnalyzer : IPageAnalyzer
    {
        private ILogger Logger = LogManager.GetLogger(nameof(ZhaopinLagouListPageAnalyzer));

        public const string SiteIdValue = SiteIndex.PlantCsdb;

        public const string TargetPageFlagValue = "lagou.list";

        public Site Site { get; }

        public string SiteId => SiteIdValue;

        public string Topic => "lagou.list";

        public string TargetPageFlag => TargetPageFlagValue;

        public ZhaopinLagouListPageAnalyzer()
        {
            Site = SiteConfiguration.Instance.GetSite(SiteId);
        }



        public PageResult Analyze(Response response)
        {
            var request = response.Request;

            if (string.IsNullOrWhiteSpace(response.Content))
                return PageResult.EmptyResponse(/*Site.Topic,*/ request, response, Topic);
            var ajaxResult = Evol.Utilities.Serialization.JsonUtility.Deserialize<LagouListAjaxResult>(response.Content);

            var newRequests = FindNewRequest(ajaxResult, request);
            var pageResult = new PageResult
            {
                Request = request,
                Response = response,
                NewRequests = newRequests,
                Topic = Topic
            };
            return pageResult;
        }

        private IEnumerable<Request> FindNewRequest(LagouListAjaxResult jsonResult, Request request)
        {
            if (!jsonResult.Success)
                return null;

            var newRequests = new List<Request>();
            var positions = jsonResult.Content?.PositionResult?.JobResult;
            if (positions == null)
                return null;
            foreach (var item in positions)
            {
                var href = "https://www.lagou.com/jobs/" + item.PositionId + ".html";
                newRequests.Add(new Request(href) { UrlType = UrlType.Navigation, SiteId = request.SiteId, HttpMethod = request.HttpMethod, PageType = request.PageType, Topic = TargetPageFlagValue });
                continue;
            }
            return newRequests;
        }

    }
}
