using Plunder.Plugin.Repositories;
using Plunder.Compoment.Models;

namespace Plunder.MemoryStorage.Repositories
{
    public class AccessLogRepository : BaseMemoryDbRepository<AccessLog>, IAccessLogRepository
    {

    }
}
