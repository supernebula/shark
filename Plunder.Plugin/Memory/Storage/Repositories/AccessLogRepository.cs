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
    public class AccessLogRepository : IAccessLogRepository
    {
        private MemoryData<AccessLog, string> _collection => MemoryData<AccessLog, string>.Instance;

        public Task AddAsync(AccessLog item)
        {
            _collection.Add(item);
            return Task.FromResult(1);
        }

        public Task AddBatchAsync(IEnumerable<AccessLog> items)
        {
            _collection.AddBatch(items);
            return Task.FromResult(1);
        }

        public Task<bool> DeleteAsync(string id)
        {
            _collection.DeleteBy(e => e.Id == id);
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBatchAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByAsync(Expression<Func<AccessLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<AccessLog> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AccessLog> FindOneAsync(Expression<Func<AccessLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AccessLog item)
        {
            throw new NotImplementedException();
        }
    }
}
