using Evol.Common;
using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting.Models
{
    public class ExtractRuleEntity : ExtractRule, IEntity<Guid>
    {
        public DateTime CreateTime { get; set; }
    }
}
