using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Test.Pipeline
{
    public class ExecutePipeline
    {
        private List<IPipelineModule> _modules;

        public ExecutePipeline()
        {
            _modules = new List<IPipelineModule>();
            _modules.Add(new DbModule());
            _modules.Add(new ConsoleModule());
            _modules.Add(new FileModule());

        }

        public void Inject(PageResult data)
        {
            var i = 1;
            foreach (IPipelineModule module in _modules)
            {
                module.ProcessAsync(data, i);
                i++;
            }

        }
    }
}
