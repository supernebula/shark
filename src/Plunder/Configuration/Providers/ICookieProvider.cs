using Plunder.Compoment.Models;
using System;
using System.Collections.Generic;

namespace Plunder.Configuration.Providers
{
    public interface ICookieProvider
    {
        IEnumerable<Cookie> GetAll(Guid siteId);
    }
}
