using Evol.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Storage.MongoDB.Entities
{
    public class PlantPhoto : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string LatinName { get; set; }

        public string SourceSite { get; set; }

        public string ThumbUrl { get; set; }

        public string NormalUrl { get; set; }

        public string ThumbPath { get; set; }

        public string NormalPath { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
