using System.IO;
using System.Net;
using System.Text;

namespace Plunder.Compoment
{
    public class Response
    {
        public Request Request { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public bool IsSuccessCode { get; set; }

        public string CharSet { get; set; }

        public Encoding Encoding { get; set; }

        public string Content { get; set; }

        public Stream StreamContent { get; set; }

        /// <summary>
        /// 耗时，毫秒
        /// </summary>
        public long ElapsedTime { get; set; }

        public string Downloader { get; set; }
    }
}
