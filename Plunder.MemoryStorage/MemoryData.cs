using Evol.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Plunder.MemoryStorage
{
    public class MemoryData<T, TKey> where T : IEntity<TKey>
    {


        private static MemoryData<T, TKey> _instance;
        public static MemoryData<T, TKey> Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MemoryData<T, TKey>();
                return _instance;
            }

        }

        private ConcurrentDictionary<TKey, T> _dic = new ConcurrentDictionary<TKey, T>();


        public bool Add(T item)
        {
            return _dic.TryAdd(item.Id, item);
        }

        public void AddBatch(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _dic.TryAdd(item.Id, item);
            }
        }

        public bool Update(T item)
        {
            T value;
            return _dic.TryGetValue(item.Id, out value) && _dic.TryUpdate(item.Id, item, value);
        }

        public bool Delete(TKey id)
        {
            T delete;
            return _dic.TryRemove(id, out delete);
        }

        public bool Delete(T item)
        {
            T delete;
            return _dic.TryRemove(item.Id, out delete);
        }

        public void DeleteBatch(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                T delete;
                _dic.TryRemove(id, out delete);
            }
        }

        public void DeleteBy(Func<T, bool> predicate)
        {
            var ids = _dic.Where(kv => predicate.Invoke(kv.Value)).Select(kv => kv.Key).ToList();
            foreach (var id in ids)
            {
                T delete;
                _dic.TryRemove(id, out delete);
            }
        }

        public T Find(TKey id)
        {
            T value;
            _dic.TryGetValue(id, out value);
            return value;
        }

        public T FindOne(Func<T, bool> predicate)
        {
            var id = _dic.Where(kv => predicate.Invoke(kv.Value)).Select(kv => kv.Key).FirstOrDefault();
            T value;
            _dic.TryGetValue(id, out value);
            return value;
        }

        public IEnumerable<T> Select(Func<T, bool> predicate)
        {
            return _dic.Where(kv => predicate.Invoke(kv.Value)).Select(kv => kv.Value).ToList();
        }

        public IPaged<T> Paged(Func<T, bool> predicate, int pageIndex, int pageSize)
        {
            var query = _dic.Where(kv => predicate.Invoke(kv.Value)).Select(kv => kv.Value);
            if (predicate != null)
                query = query.Where(predicate);
            var enumerable = query as T[] ?? query.ToArray();
            var total = enumerable.Count();
            var list = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var result = new Paged<T>() {
                PageTotal = total % pageSize == 0 ? total / pageSize : total / pageSize + 1,
                RecordTotal = total,
                Index = pageIndex,
                Size = pageSize
            };
            result.AddRange(list);
            return result;
        }




    }

}
