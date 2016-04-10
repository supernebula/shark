using System.Net;

namespace Plunder.Compoment
{
    public class Response
    {
        public Request Request { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public bool IsSuccessCode { get; set; }

        public string Content { get; set; }

        public int MillisecondTime { get; set; }

        public string Downloader { get; set; }
    }
}
