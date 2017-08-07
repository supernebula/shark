using Evol.MongoDB.Repository;
using Plunder.Plugin.Repositories;
using Plunder.Setting.Data.Interfaces;
using Plunder.Setting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting.Data.Mongo
{
    public class DomainRepository : BaseMongoDbRepository<DomainEntity, PlunderSettingContext>, IRepository<DomainEntity>, IDomainRepository
    {
        public DomainRepository(IMongoDbContextProvider mongoDbContextProvider) : base(mongoDbContextProvider)
        { }
    }
}
