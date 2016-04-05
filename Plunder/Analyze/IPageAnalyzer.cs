using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;
using Plunder.Scheduler;

namespace Plunder.Analyze
{
    public interface IPageAnalyzer
    {
        Guid Id { get; }
        PageResult Analyze(Request request, Response response);

        Site Site { get; }
    }
}
