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
    public class PageRepository : IPageRepository
    {
        public Task AddAsync(Page item)
        {
            throw new NotImplementedException();
        }

        public Task AddBatchAsync(IEnumerable<Page> items)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBatchAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByAsync(Expression<Func<Page, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Page> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Page> FindOneAsync(Expression<Func<Page, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Page item)
        {
            throw new NotImplementedException();
        }
    }
}
