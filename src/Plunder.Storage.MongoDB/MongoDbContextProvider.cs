using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evol.MongoDB.Repository;

namespace Plunder.Storage.MongoDB
{
    class MongoDbContextProvider : IMongoDbContextProvider
    {
        public NamedMongoDbContext Get<TDbContext>() where TDbContext : NamedMongoDbContext, new()
        {
            throw new NotImplementedException();
        }
    }
}
