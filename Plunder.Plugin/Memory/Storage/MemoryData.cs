using Evol.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Plunder.Plugin.Memory.Storage
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

        private ConcurrentBag<T> _collecton = new ConcurrentBag<T>();

        public IEnumerable<T> Collecton()
        {
            return _collecton;
        }

        public void Add(T item)
        {
            _collecton.Add(item);
        }

        public void AddBatch(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _collecton.Add(item);
            }
            
        }

        public bool Update(T item)
        {
            T delete;
            if (!_collecton.TryTake(out delete))
                return false;
            _collecton.Add(item);
            return true;
        }

        public bool Delete(T item)
        {
            T delete;
            return _collecton.TryTake(out delete);
        }

        public void DeleteBatch(IEnumerable<TKey> ids)
        {
            var items = _collecton.Where(e => ids.Contains(e.Id)).ToList();
            items.ForEach(e => Delete(e));
        }

        public void DeleteBy(Func<T, bool> predicate)
        {
            var items = _collecton.Where(predicate).ToList();
            items.ForEach(e => Delete(e));
        }

        [Obsolete]
        public T FindOne(Func<T, bool> predicate)
        {
            return _collecton.SingleOrDefault(predicate);
        }

        public IEnumerable<T> Select(Func<T, bool> predicate)
        {
            return _collecton.Where(predicate).ToList();
        }

        public IPaged<T> Paged(Func<T, bool> predicate, int pageIndex, int pageSize)
        { 
            IEnumerable<T> query = _collecton;
            if (predicate != null)
                query = _collecton.Where(predicate);
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
