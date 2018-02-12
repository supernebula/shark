using Evol.Common;
using System;

namespace Plunder.Storage.MongoDB.Entities
{
    public class LagouJob : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string industry { get; set;}

        public string financing { get; set; }

        public int StaffNumber { get; set; }

        public string SiteUrl { get; set; }

        public string Site { get; set; }

        public string City { get; set; }

        public string Title { get; set; }

        public int MinSalary { get; set; }

        public int MaxSalary { get; set; }

        public string WorkYear { get; set; }

        public string Education { get; set; }

        public string PositionInfo { get; set; }

        public string Address { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
