using Evol.Common;
using Plunder.Compoment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting.Models
{
    public class DomainEntity : Domain, IEntity<Guid>
    {
        public DateTime CreateTime { get; set; }
    }
}
