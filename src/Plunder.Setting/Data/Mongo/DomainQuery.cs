using Evol.MongoDB.Repository;
using Plunder.Plugin.QueryEntries;
using Plunder.Setting.Data.Interfaces;
using Plunder.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Plunder.Setting.Data.Mongo
{
    public class DomainQuery : BaseQueryEntry<DomainEntity, PlunderSettingContext>, IQueryEntry<DomainEntity, Guid>, IDomainQuery
    {
        public new Task<List<DomainEntity>> SelectAsync(Expression<Func<DomainEntity, bool>> predicate, Expression<Func<DomainEntity, Guid>> orderByKeySelector, bool isDescending = true)
        {
            throw new NotImplementedException();
        }
    }
}
