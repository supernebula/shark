using Plunder.Plugin.QueryEntries;
using Plunder.Compoment.Models;

namespace Plunder.MemoryStorage.QueryEntries
{
    public class AccessLogQueryEntry : BaseQueryEntry<AccessLog, string>, IAccessLogQueryEntry
    {
    }
}
