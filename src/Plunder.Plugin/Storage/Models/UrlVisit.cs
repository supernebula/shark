using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Models
{
    public class UrlVisit
    {
        public string Id { get; set; }

        public string SiteId { get; set; }

        public HtmlType HtmlType { get; set; }

        public string Domain { get; set; }

        public string Uri { get; set; }

        public string UriSign { get; set; }

        public string Content { get; set; }

        public bool IsFetched { get; set; }

        /// <summary>
        /// millisecond
        /// </summary>
        public long Elapsed { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
