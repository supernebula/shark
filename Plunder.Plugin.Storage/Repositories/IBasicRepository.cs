using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Repositories
{
    public interface IBasicRepository<T> : IDisposable where T : class 
    {
        IQueryable<T> Query();
        void Insert(T item);

        void Delete(T item);

        void Update(T item);

        T Fetch(Guid id);

        Task<T> FetchAsync(Guid id);

        IEnumerable<T> Retrieve(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> RetrieveAsync(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Paged(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);

    }
}
