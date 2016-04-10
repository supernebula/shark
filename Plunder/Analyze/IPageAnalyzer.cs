using System;
using System.Collections.Generic;
using System.Linq;
using Plunder.Compoment;

namespace Plunder.Analyze
{
    public interface IPageAnalyzer
    {
        Guid Id { get; }
        PageResult Analyze(Request request, Response response);

        Site Site { get; }
    }
}
