using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Evol.MongoDB.Repository
{
    public abstract class BaseMongoDBRepository<T>
    {
        public async Task<T> FindAsync()
        {
            throw new NotImplementedException("");
        }
    }
}
