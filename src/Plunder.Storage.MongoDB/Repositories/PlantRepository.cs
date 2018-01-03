using Evol.Common;
using Evol.MongoDB.Repository;
using Plunder.Plugin.Repositories;
using Plunder.Storage.MongoDB.Entities;
using System;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class PlantRepository : BaseMongoDbRepository<Plant, PlunderMongoDBContext>, IRepository<Plant>
    {
        public PlantRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        {
        }
    }
}
