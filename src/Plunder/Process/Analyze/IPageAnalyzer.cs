using Plunder.Compoment;

namespace Plunder.Process.Analyze
{
    public interface IPageAnalyzer
    {
        Site Site { get; }

        string SiteId { get;  }

        string PageTag { get; }

        PageResult Analyze(Response response);
    }
}
