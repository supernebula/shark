using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;
using Plunder.Compoment.Models;
using Plunder.Plugin.QueryEntries;

namespace Plunder.Storage.MongoDB.QueryEntries
{
    public class AccessRecordQueryEntry : BaseQueryEntry<AccessRecord, PlunderMongoDBContext>, IQueryEntry<AccessRecord, string>
    {
        public Task<AccessRecord> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public new Task<List<AccessRecord>> SelectAsync(Expression<Func<AccessRecord, bool>> predicate, Expression<Func<AccessRecord, string>> orderByKeySelector, bool isDescending = true)
        {
            throw new NotImplementedException();
        }
    }
}
