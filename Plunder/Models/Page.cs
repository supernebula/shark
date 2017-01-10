using System;

namespace Plunder.Models
{
    public class Page
    {
        public string Topic { get; set; }
        public string Uri { get; set; }

        public string Domain { get; set; }

        public string UriSign { get; set; }

        public string Text { get; set; }

        public bool IsFetched { get; set; }

        /// <summary>
        /// millisecond
        /// </summary>
        public int Elapsed { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
