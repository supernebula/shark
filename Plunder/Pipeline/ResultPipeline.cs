using Plunder.Compoment;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;


namespace Plunder.Pipeline
{
    public class ResultPipeline
    {
        private readonly ConcurrentBag<IResultPipelineModule> _modules;


        public ResultPipeline()
        {
            _modules = new ConcurrentBag<IResultPipelineModule>();
        }

        public ResultPipeline(IEnumerable<IResultPipelineModule> modules)
        {
            _modules = new ConcurrentBag<IResultPipelineModule>(modules);
        }

        public void RegisterModule(IResultPipelineModule module)
        {
            if (_modules.Any(e => e.GetType() == module.GetType()))
                return;
            _modules.Add(module);
        }

        public void RegisterModule(IEnumerable<IResultPipelineModule> modules)
        {
            modules.ToList().ForEach(e => RegisterModule(e));
        }

        public void Inject<T>(PageResult<T> data)
        {
            foreach (IResultPipelineModule module in _modules)
            {
                module.ProcessAsync(data);
            }
        }
    }
}
