//using Plunder.Plugin.QueryEntries;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Evol.Common;
//using Plunder.Models;
//using System.Linq.Expressions;

//namespace Plunder.Plugin.Memory.Storage.QueryEntries
//{
//    public class PageQueryEntry : IPageQueryEntry
//    {
//        public Task<Page> FindAsync(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Page> FindOneAsync(Expression<Func<Page, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IPaged<Page>> PagedSelectAsync(Expression<Func<Page, bool>> predicate, int pageIndex, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Page>> SelectAsync<TKey>(Expression<Func<Page, bool>> predicate, Expression<Func<Page, TKey>> orderByKeySelector, bool isDescending = true)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
