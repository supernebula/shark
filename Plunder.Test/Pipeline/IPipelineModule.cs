using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Test.Pipeline
{
    public interface IPipelineModule
    {
        Task<int> ProcessAsync(PageResult data, int serialNumber);
    }
}

