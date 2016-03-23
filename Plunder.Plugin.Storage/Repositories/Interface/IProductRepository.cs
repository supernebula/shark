using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Plugin.Storage.Models;

namespace Plunder.Plugin.Storage.Repositories
{
    public interface IProductRepository
    {
        void Insert(Product item);
    }
}
