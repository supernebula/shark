using Plunder.Compoment.Models;
using System.Collections.Generic;

namespace Plunder.Configuration.Providers
{
    public interface IHttpProxyProvider
    {
        IEnumerable<HttpProxy> GetAll();
    }
}
