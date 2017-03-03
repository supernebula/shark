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
//    public class AccessLogQueryEntry : IAccessLogQueryEntry
//    {
//        public Task<AccessLog> FindAsync(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<AccessLog> FindOneAsync(Expression<Func<AccessLog, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IPaged<AccessLog>> PagedSelectAsync(Expression<Func<AccessLog, bool>> predicate, int pageIndex, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<AccessLog>> SelectAsync<TKey>(Expression<Func<AccessLog, bool>> predicate, Expression<Func<AccessLog, TKey>> orderByKeySelector, bool isDescending = true)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
