using Plunder.Compoment.Models;
using Plunder.Compoment.Values;
using System.Collections.Generic;

namespace Plunder.Configuration.Providers
{
    public interface IHttpProxyProvider
    {
        IEnumerable<HttpProxy> GetAll(HttpProxyType type);
    }
}
