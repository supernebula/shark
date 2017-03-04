using Plunder.Compoment;

namespace Plunder.Process.Analyze
{
    public interface IPageAnalyzer
    {
        Site Site { get; }
        PageResult Analyze(Request request, Response response);
    }
}
