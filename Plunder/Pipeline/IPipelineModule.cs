using Plunder.Compoment;
using System.Threading.Tasks;

namespace Plunder.Pipeline
{
    public interface IPipelineModule
    {
        string Name { get; }

        string Description { get; }

        void Init(object context);

        Task ProcessAsync(PageResult result);

    }
}
