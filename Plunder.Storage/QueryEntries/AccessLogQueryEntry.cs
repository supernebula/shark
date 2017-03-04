using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.QueryEntries;

namespace Plunder.Storage.QueryEntries
{
    public class AccessLogQueryEntry : BaseQueryEntry<AccessLog, PlunderMongoDBContext>, IQueryEntry<AccessLog, string>
    {
        public new Task<List<AccessLog>> SelectAsync(Expression<Func<AccessLog, bool>> predicate, Expression<Func<AccessLog, string>> orderByKeySelector, bool isDescending = true)
        {
            throw new NotImplementedException();
        }
    }
}
