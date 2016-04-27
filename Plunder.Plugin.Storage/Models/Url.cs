using Plunder.Compoment;

namespace Plunder.Plugin.Storage.Models
{
    public class Url : BasicEntity
    {
        public string SiteId { get; set; }

        public string Channel { get; set; }

        public string Hash { get; set; }

        public string Value { get; set; }

        public string HttpMethod { get; set; }

        public UrlType UrlType { get; set; }

        public UrlStatusType Status { get; set; }

        public int AlreadyRetryCount { get; set; }
    }
}
