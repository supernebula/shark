using Plunder.Compoment;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;


namespace Plunder.Pipeline
{
    public class ResultPipeline
    {
        private readonly ConcurrentBag<IResultPipelineModule> _modules;

        private int _resultTotal;
        public int ResultTotal => _resultTotal;

        public int ModuleCount => _modules.Count;


        public ResultPipeline()
        {
            _modules = new ConcurrentBag<IResultPipelineModule>();
        }

        public ResultPipeline(IEnumerable<IResultPipelineModule> modules)
        {
            _modules = new ConcurrentBag<IResultPipelineModule>(modules);
        }

        public bool IsContainProducer()
        {
            return _modules.OfType<DefaultMomeryProducerModule>().Any();
        }


        public void RegisterModule(IResultPipelineModule module)
        {
            if (_modules.Any(e => e.GetType() == module.GetType()))
                return;
            _modules.Add(module);
        }

        public void RegisterModule(IEnumerable<IResultPipelineModule> modules)
        {
            modules.ToList().ForEach(RegisterModule);
        }

        public void Inject(PageResult data)
        {
            Interlocked.Increment(ref _resultTotal);
            foreach (IResultPipelineModule module in _modules)
            {
                module.ProcessAsync(data);
            }
        }
    }
}
