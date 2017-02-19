using Evol.MongoDB.Repository;
using Plunder.Models;
using Plunder.Plugin.Repositories;

namespace Plunder.Storage.Repositories
{
    public class AccessLogRepository : BaseMongoDbRepository<AccessLog, PlunderMongoDBContext> , IRepository<AccessLog>
    {
    }
}
