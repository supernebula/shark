using Evol.Common;
using System;
using System.Collections.Generic;

namespace Plunder.Storage.MongoDB.Entities
{
    public class Plant : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string LatinName { get; set; }

        public string ZhName { get; set; }

        public string Namer { get; set; }

        public string Locality { get; set; }

        public string ListUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
