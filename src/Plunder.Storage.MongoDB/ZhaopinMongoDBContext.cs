using Evol.MongoDB.Repository;

namespace Plunder.Storage.MongoDB
{
    public class ZhaopinMongoDBContext : NamedMongoDbContext
    {
        public ZhaopinMongoDBContext() : base("mongodb://127.0.0.1/ZhaopinCollect")
        {
        }
    }
}
