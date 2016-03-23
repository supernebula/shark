using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Repositories
{
    public class BasicEntityFrameworkRepository<T, V> : IBasicRepository<T> where V : DbContext
    {

        public V DbContext { get; set; }
        public BasicEntityFrameworkRepository()
        {

        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public T Fetch(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FetchAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Retrieve(Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> RetrieveAsync(Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
