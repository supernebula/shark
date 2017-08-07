using Evol.MongoDB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting.Data
{
    public class PlunderSettingContext : NamedMongoDbContext
    {
        public PlunderSettingContext() : base("mongodb://127.0.0.1/plunder")
        {
        }
    }
}
