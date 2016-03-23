using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Plunder.Plugin.Storage.Repositories
{
    public interface IBasicRepository<T>
    {
        IQueryable<T> Query();

        void Insert(T item);

        T Fetch(Guid id);

        Task<T> FetchAsync(Guid id);

        IEnumerable<T> Retrive(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> RetriveAsync(Expression<Func<T, bool>> expression);

        void Update(T item);

        void Delete(Guid id);


    }
}
