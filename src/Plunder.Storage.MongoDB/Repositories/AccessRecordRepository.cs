using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.Repositories;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class AccessRecordRepository : BaseMongoDbRepository<AccessRecord, PlunderMongoDBContext>, IRepository<AccessRecord>
    {
        public AccessRecordRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        {
        }
    }
}
