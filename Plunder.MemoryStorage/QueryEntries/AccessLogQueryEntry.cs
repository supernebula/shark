using Plunder.Plugin.QueryEntries;
using Plunder.Models;

namespace Plunder.MemoryStorage.QueryEntries
{
    public class AccessLogQueryEntry : BaseQueryEntry<AccessLog, string>, IAccessLogQueryEntry
    {
    }
}
