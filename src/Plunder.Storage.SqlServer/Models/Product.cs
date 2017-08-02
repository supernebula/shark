using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Storage.SqlServer.Models
{
    public class Product : BasicEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string PicUrl { get; set; }

        public string Uri { get; set; }

        public int CommentCount { get; set; }

        public string SiteName { get; set; }

        public string SiteDomain { get; set; }

        public double ElapsedSecond { get; set; }

        public string Downloader { get; set; }
    }
}
