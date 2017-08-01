using Evol.Common;
using System;
using System.Net;

namespace Plunder.Compoment.Models
{
    public class AccessRecord : IEntity<string>
    {
        public virtual string Id { get; set; }

        public string Domian { get; set; }

        public string Url { get; set; }

        public string UrlSign { get; set; }

        public UrlType UrlType { get; set; }

        public string PageType { get; set; }

        public int NewTargetUrlNum { get; set; }

        public string Downloader { get; set; }

        public bool IsFetched { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessCode { get; set; }

        /// <summary>
        /// 毫秒
        /// </summary>
        public int Elapsed { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }


    }
}
