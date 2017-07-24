using Plunder.Compoment;

namespace Plunder.Core
{
    public interface IResultItemPipeline
    {
        int ModuleCount { get; }

        bool IsContainProducer();

        void Inject(PageResult data);
    }
}
