using Evol.Common;
using System;
using System.Collections.Generic;

namespace Plunder.Storage.MongoDB.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Domain { get; set; }

        public string Url { get; set; }

        public string UrlSign { get; set; }

        public List<string> PicUrl { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
