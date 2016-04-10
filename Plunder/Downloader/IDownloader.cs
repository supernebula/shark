using System.Threading.Tasks;
using Plunder.Compoment;

namespace Plunder.Downloader
{
    public interface IDownloader
    {
        string Topic { get; }

        bool IsDefault { get; set; }

        bool IsAllowDownload();

        int TaskCount();

        Task<Response> DownloadAsync(Request request);
    }
}
