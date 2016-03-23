using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.Repositories
{
    public class PlunderDbContext : DbContext
    {
        public PlunderDbContext() : base("name=PlunderDBContext")
        {
        }
    }
}
