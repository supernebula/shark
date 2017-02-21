using Plunder.Plugin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Models;
using System.Linq.Expressions;

namespace Plunder.Plugin.Memory.Storage.Repositories
{
    public class AccessLogRepository : BaseMemoryDbRepository<AccessLog>, IAccessLogRepository
    {
        Task<bool> IRepository<AccessLog>.DeleteBatchAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<AccessLog>.DeleteByAsync(Expression<Func<AccessLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<AccessLog>.UpdateAsync(AccessLog item)
        {
            throw new NotImplementedException();
        }
    }
}
