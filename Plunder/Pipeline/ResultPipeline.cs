using Plunder.Compoment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Pipeline
{
    public class ResultPipeline
    {
        List<IResultPipelineModule> _modules;

        public ResultPipeline(IEnumerable<IResultPipelineModule> modules)
        {
            _modules.AddRange(modules);
        }

        public void Inject<T>(PageResult<T> result)
        {
                
        }
    }
}
