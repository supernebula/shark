//using Evol.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Plunder.Plugin.Memory.Storage.Repositories
//{
//    public class BaseMemoryDbRepository<T> where T : IEntity<string>
//    {
//        private MemoryData<T, string> _collection => MemoryData<T, string>.Instance;

//        public Task AddAsync(T item)
//        {
//            _collection.Add(item);
//            return Task.FromResult(1);
//        }

//        public Task AddBatchAsync(IEnumerable<T> items)
//        {
//            _collection.AddBatch(items);
//            return Task.FromResult(1);
//        }

//        public Task<bool> DeleteAsync(string id)
//        {
//            var flag = _collection.Delete(id);
//            return Task.FromResult(flag);
//        }

//        public Task<bool> DeleteBatchAsync(IEnumerable<string> ids)
//        {
//            foreach (var id in ids)
//            {
//                _collection.Delete(id);
//            }
//            return Task.FromResult(true);
//        }

//        public Task<bool> DeleteByAsync(Expression<Func<T, bool>> predicate)
//        {
//            var func = predicate.Compile();
//            _collection.DeleteBy(func);
//            return Task.FromResult(true);
//        }

//        public Task<T> FindAsync(string id)
//        {
//            var value = _collection.Find(id);
//            return Task.FromResult(value);
//        }

//        public Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
//        {
//            var func = predicate.Compile();
//            var value = _collection.FindOne(func);
//            return Task.FromResult(value);
//        }

//        public Task<bool> UpdateAsync(T item)
//        {
//            _collection.Update(item);
//            return Task.FromResult(true);
//        }

//        public Task<IPaged<T>> PagedSelectAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
//        {
//            var  result =_collection.Paged(predicate.Compile(), pageIndex, pageSize);
//            return Task.FromResult(result);
//        }

//        public Task<List<T>> SelectAsync<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderByKeySelector, bool isDescending = true)
//        {
//            var list = _collection.Select(predicate.Compile()).OrderBy(orderByKeySelector.Compile()).ToList();
//            return Task.FromResult(list);
//        }
//    }
//}
