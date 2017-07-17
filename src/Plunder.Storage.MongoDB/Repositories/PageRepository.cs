using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.Repositories;

namespace Plunder.Storage.MongoDB.Repositories
{
    public class PageRepository : BaseMongoDbRepository<Page, PlunderMongoDBContext>, IRepository<Page>
    {
    }
}
