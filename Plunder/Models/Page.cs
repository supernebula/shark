using Evol.Common;
using System;

namespace Plunder.Models
{
    /// <summary>
    /// Save to MongoDb
    /// </summary>
    public class Page : IEntity<string>
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public string Domain { get; set; }

        public string Uri { get; set; }

        public string UriSign { get; set; }

        public string Content { get; set; }

        public bool IsFetched { get; set; }

        /// <summary>
        /// millisecond
        /// </summary>
        public int Elapsed { get; set; }

        public DateTime CreateTime { get; set; }

        
    }
}
