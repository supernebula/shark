using Evol.MongoDB.Repository;

namespace Plunder.Storage
{
    public class PlunderMongoDBContext : NamedMongoDbContext
    {
        public PlunderMongoDBContext():base("plunderConnectionString")
        {
        }
    }
}
