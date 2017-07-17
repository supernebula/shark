using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Storage.SqlServer.Models;

namespace Plunder.Storage.SqlServer.Repositories
{
    public class UrlRepository : BasicEntityFrameworkRepository<Url, PlunderDbContext>
    {
    }
}
