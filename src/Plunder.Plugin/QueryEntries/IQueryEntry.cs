
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Evol.Common;

namespace Plunder.Plugin.QueryEntries
{
    public interface IQueryEntry
    {
    }

    public interface IQueryEntry<T, TKey> : IQueryEntry
    {

        Task<T> FindAsync(string id);

        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> SelectAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderByKeySelector, bool isDescending = true);

        Task<IPaged<T>> PagedSelectAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
    }
}
