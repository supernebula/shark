using Plunder.Process.Analyze;

namespace Plunder.Core
{
    public interface IPageAnalyzerFactory
    {
        IPageAnalyzer Create(string siteId, string topic);

        int Count { get; }

        bool Any();
    }
}
