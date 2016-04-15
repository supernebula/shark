using Plunder.Compoment;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;


namespace Plunder.Pipeline
{
    public class ResultPipeline
    {
        private readonly ConcurrentBag<IResultPipelineModule> _modules;

        public int ResultTotal { get; private set; }


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

        public void Inject(PageResult data)
        {
            ResultTotal++; //并发问题
            foreach (IResultPipelineModule module in _modules)
            {
                module.ProcessAsync(data);
            }
        }
    }
}
