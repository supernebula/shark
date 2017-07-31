﻿using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.Repositories;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class AccessLogRepository : BaseMongoDbRepository<AccessLog, PlunderMongoDBContext>, IRepository<AccessLog>
    {
        public AccessLogRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        {
        }
    }
}
