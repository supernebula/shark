using Plunder.Compoment;
using Plunder.Download;
using System;
using System.Net;

namespace Plunder.Plugin.Storage.Models
{
    public class UrlFind
    {
        public string Id { get; set; }

        public string SiteId { get; set; }

        public PageType PageType { get; set; }

        public string Domian { get; set; }

        public string Url { get; set; }

        public string UrlSign { get; set; }

        public bool IsVisited { get; set; }

        public DateTime VisitTime { get; set; }

        public bool IsSuccess { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 耗时毫秒数
        /// </summary>
        public long Elapsed { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
