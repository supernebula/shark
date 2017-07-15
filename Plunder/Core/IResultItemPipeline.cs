using Plunder.Compoment;

namespace Plunder.Core
{
    public interface IResultItemPipeline
    {
        bool IsContainProducer();

        void Inject(PageResult data);
    }
}
