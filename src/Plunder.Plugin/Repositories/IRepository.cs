using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Plunder.Plugin.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository
    {
        Task<T> FindAsync(Guid id);

        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T item);

        Task AddBatchAsync(IEnumerable<T> items);

        Task<bool> UpdateAsync(T item);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> DeleteBatchAsync(IEnumerable<Guid> ids);

        Task<bool> DeleteByAsync(Expression<Func<T, bool>> predicate);
    }
}
