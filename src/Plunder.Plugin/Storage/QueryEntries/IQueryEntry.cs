using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.QueryEntries
{
    public interface IQueryEntry<TEntity>
    {
        Task<TEntity> FindAsync(Guid id);

        Task<TEntity> SelectAsync<TParam>(TParam param = null) where TParam : class;

        Task<TEntity> PagedAsync<TParam>(int pageIndex, int pageSize, TParam param = null) where TParam : class;
    }
}
