using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Storage.SqlServer.Maps;
using Plunder.Storage.SqlServer.Models;

namespace Plunder.Storage.SqlServer.Repositories
{
    public class PlunderDbContext : DbContext
    {
        public PlunderDbContext() : base("name=PlunderDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        static PlunderDbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PlunderDbContext>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new UrlMap());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

        }
    }
}
