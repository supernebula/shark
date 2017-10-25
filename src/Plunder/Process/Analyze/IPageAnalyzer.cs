using Plunder.Compoment;

namespace Plunder.Process.Analyze
{
    public interface IPageAnalyzer
    {
        Site Site { get; }

        string SiteId { get;  }

        string Topic { get; }

        string TargetPageFlag { get; }

        PageResult Analyze(Response response);
    }
}
