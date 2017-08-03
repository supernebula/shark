using Plunder.Compoment.Models;
using System.Collections.Generic;

namespace Plunder.Configuration.Providers
{
    public interface IUserAgentProvider
    {
        IEnumerable<UserAgent> GetAll();
    }
}
