using Plunder.Compoment;
using System;
using System.Collections.Generic;

namespace Plunder.Configuration.Providers
{
    public interface IExtractRuleProvider
    {
        IEnumerable<ExtractRule> GetAll(Guid siteId);
    }
}
