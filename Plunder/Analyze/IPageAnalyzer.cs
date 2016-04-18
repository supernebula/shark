using Plunder.Compoment;

namespace Plunder.Analyze
{
    public interface IPageAnalyzer
    {
        Site Site { get; }
        PageResult Analyze(Request request, Response response);
    }
}
