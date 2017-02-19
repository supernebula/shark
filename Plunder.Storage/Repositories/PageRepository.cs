using Evol.MongoDB.Repository;
using Plunder.Models;
using Plunder.Plugin.Repositories;

namespace Plunder.Storage.Repositories
{
    public class PageRepository : BaseMongoDbRepository<Page, PlunderMongoDBContext>, IRepository<Page>
    {
    }
}
