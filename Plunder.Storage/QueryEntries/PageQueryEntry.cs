using Evol.MongoDB.Repository;
using Plunder.Models;
using Plunder.Plugin.QueryEntries;

namespace Plunder.Storage.QueryEntries
{
    public class PageQueryEntry : BaseQueryEntry<Page, PlunderMongoDBContext>, IQueryEntry<Page>
    {
    }
}
