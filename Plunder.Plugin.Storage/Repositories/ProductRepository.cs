using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Plugin.Storage.Models;

namespace Plunder.Plugin.Storage.Repositories
{
    public class ProductRepository : BasicEntityFrameworkRepository<Product, PlunderDbContext>, IProductRepository
    {
    }
}
