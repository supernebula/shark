using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Models
{
    public class BasicEntity : IEntity
    {
        public DateTime CreateTime { get; set; }

        public Guid Id { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
