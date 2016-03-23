using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Plugin.Storage.Maps;
using Plunder.Plugin.Storage.Models;

namespace Plunder.Plugin.Storage.Repositories
{
    public class PlunderDbContext : DbContext
    {
        public PlunderDbContext() : base("name=PlunderDBContext")
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
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

        }
    }
}
