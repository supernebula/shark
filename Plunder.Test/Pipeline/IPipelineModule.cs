using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Test.Pipeline
{
    public interface IPipelineModule
    {
        Task<int> ProcessAsync(DataResult data, int serialNumber);
    }
}

