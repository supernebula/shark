using Evol.MongoDB.Repository;

namespace Plunder.Storage.MongoDB
{
    public class PlunderMongoDBContext : NamedMongoDbContext
    {
        public PlunderMongoDBContext():base("plunderConnectionString")
        {
        }
    }
}
