using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.QueryEntries;

namespace Plunder.Storage.MongoDB.QueryEntries
{
    public class PageQueryEntry : BaseQueryEntry<Page, PlunderMongoDBContext>, IQueryEntry<Page, string>
    {
        public new Task<List<Page>> SelectAsync(Expression<Func<Page, bool>> predicate, Expression<Func<Page, string>> orderByKeySelector, bool isDescending = true)
        {
            throw new NotImplementedException();
        }
    }
}
