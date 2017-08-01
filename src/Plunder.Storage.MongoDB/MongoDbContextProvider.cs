using Evol.MongoDB.Repository;

namespace Plunder.Storage.MongoDB
{
    public class MongoDbContextProvider : IMongoDbContextProvider
    {
        public NamedMongoDbContext Get<TDbContext>() where TDbContext : NamedMongoDbContext, new()
        {
            var context = AppConfig.Current.IocManager.GetService<TDbContext>();
            return context;
        }
    }
}
