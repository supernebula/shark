using Evol.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plunder.MemoryStorage.Repositories;

namespace Plunder.MemoryStorage.QueryEntries
{
    public  class BaseQueryEntry<T, TKey> where T : IEntity<string>
    {

        public BaseMemoryDbRepository<T> BaseRepository = new BaseMemoryDbRepository<T>();

        public async Task<T> FindAsync(string id)
        {
            return await BaseRepository.FindAsync(id);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await BaseRepository.FindOneAsync(predicate);
        }

        public async Task<IPaged<T>> PagedSelectAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            return await BaseRepository.PagedSelectAsync(predicate, pageIndex, pageSize);
        }

        public async Task<List<T>> SelectAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderByKeySelector, bool isDescending = true)
        {
            return await BaseRepository.SelectAsync(predicate, orderByKeySelector);
        }

    }
}
