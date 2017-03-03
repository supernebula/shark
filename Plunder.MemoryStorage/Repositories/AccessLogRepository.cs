using Plunder.Plugin.Repositories;
using Plunder.Models;

namespace Plunder.MemoryStorage.Repositories
{
    public class AccessLogRepository : BaseMemoryDbRepository<AccessLog>, IAccessLogRepository
    {

    }
}
