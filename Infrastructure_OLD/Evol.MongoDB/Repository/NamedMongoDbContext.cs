using System.Configuration;
using MongoDB.Driver;

namespace Evol.MongoDB.Repository
{
    public class NamedMongoDbContext
    {
        public string ConnectionStringName { get; private set; }

        public string ConnectionString { get; private set; }

        public IMongoClient MongoClient { get; private set; }

        public IMongoDatabase Database { get; private set; }

        protected NamedMongoDbContext(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            var mongoUrl = new MongoUrl(ConnectionString);
            MongoClient = new MongoClient(mongoUrl);
            Database = MongoClient.GetDatabase(mongoUrl.DatabaseName);
        }
    }
}
