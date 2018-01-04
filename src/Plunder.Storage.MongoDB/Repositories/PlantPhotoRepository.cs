using Evol.Common;
using Evol.MongoDB.Repository;
using Plunder.Plugin.Repositories;
using Plunder.Storage.MongoDB.Entities;
using System;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class PlantPhotoRepository : BaseMongoDbRepository<PlantPhoto, PlunderMongoDBContext>, IRepository<PlantPhoto>
    {
        public PlantPhotoRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        {
        }
    }
}

