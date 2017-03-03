using Plunder.Plugin.QueryEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evol.Common;
using Plunder.Models;
using System.Linq.Expressions;

namespace Plunder.MemoryStorage.QueryEntries
{
    public class AccessLogQueryEntry : BaseQueryEntry<AccessLog, string>, IAccessLogQueryEntry
    {
    }
}
