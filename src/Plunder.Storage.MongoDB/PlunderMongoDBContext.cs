using Evol.MongoDB.Repository;

namespace Plunder.Storage.MongoDB
{
    public class PlunderMongoDBContext : NamedMongoDbContext
    {
        public PlunderMongoDBContext():base("mongodb://127.0.0.1/plunder")
        {
        }
    }
}
