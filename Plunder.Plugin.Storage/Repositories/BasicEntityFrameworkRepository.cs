using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Repositories
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class BasicEntityFrameworkRepository<T, TDbContext> : IBasicRepository<T> where TDbContext : DbContext, new() where T : class
    {

        public TDbContext Context { get; private set; }
        public BasicEntityFrameworkRepository()
        {
            Context = new TDbContext();
        }

        protected DbSet<T> DbSet
        {
            get
            {
                return Context.Set<T>();
            }
        }

        public void Insert(T item)
        {
            DbSet.Add(item);
            Context.SaveChanges();
        }

        public void InsertRange(IEnumerable<T> items)
        {
            DbSet.AddRange(items);
            Context.SaveChanges();
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
            Context.SaveChanges();
        }

        public T Fetch(Guid id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> FetchAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }



        public IQueryable<T> Query()
        {
            return DbSet.AsQueryable();
        }

        public IEnumerable<T> Retrieve(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task<IEnumerable<T>> RetrieveAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => DbSet.Where(predicate));
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用  

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if(Context != null)
                        Context.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BasicEntityFrameworkRepository() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public IEnumerable<T> Paged(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();


        }
        #endregion
    }
}
