using Plunder.Plugin.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Plugin.Storage.QueryEntries
{
    public interface IUrlFindQueryEntry : IQueryEntry<UrlFind>
    {
        Task<UrlFind> FindBySignAsync(string urlSign);
    }
}
