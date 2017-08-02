using Evol.MongoDB.Repository;
using Plunder.Plugin.Repositories;
using Plunder.Storage.MongoDB.Entities;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class ProductRepository : BaseMongoDbRepository<Product, PlunderMongoDBContext>, IRepository<Product>
    {
        public ProductRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        {
        }
    }
}

