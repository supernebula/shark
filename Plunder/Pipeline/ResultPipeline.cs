using Plunder.Compoment;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;


namespace Plunder.Pipeline
{
    public class ResultPipeline
    {
        private readonly ConcurrentBag<IPipelineModule> _modules;

        private int _resultTotal;
        public int ResultTotal => _resultTotal;

        public int ModuleCount => _modules.Count;


        public ResultPipeline()
        {
            _modules = new ConcurrentBag<IPipelineModule>();
        }

        public ResultPipeline(IEnumerable<IPipelineModule> modules)
        {
            _modules = new ConcurrentBag<IPipelineModule>(modules);
        }

        public bool IsContainProducer()
        {
            return _modules.OfType<DefaultMomeryProducerModule>().Any();
        }


        public void RegisterModule(IPipelineModule module)
        {
            if (_modules.Any(e => e.GetType() == module.GetType()))
                return;
            _modules.Add(module);
        }

        public void RegisterModule(IEnumerable<IPipelineModule> modules)
        {
            modules.ToList().ForEach(RegisterModule);
        }

        public void Inject(PageResult data)
        {
            Interlocked.Increment(ref _resultTotal);
            foreach (IPipelineModule module in _modules)
            {
                module.ProcessAsync(data);
            }
        }
    }
}
