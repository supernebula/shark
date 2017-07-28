using Plunder.Compoment;
using Plunder.Pipeline;

namespace Plunder.Core
{
    public interface IResultItemPipeline
    {
        int ModuleCount { get; }

        bool IsContainProducer();

        void RegisterModule(IResultPipelineModule module);

        void Inject(PageResult data);
    }
}
