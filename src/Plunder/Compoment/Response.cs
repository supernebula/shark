using System;
using System.IO;
using System.Net;
using System.Text;

namespace Plunder.Compoment
{
    public class Response
    {
        public Request Request { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public bool IsSuccessCode { get; set; }

        public string ReasonPhrase { get; set; }

        public string CharSet { get; set; }

        public Encoding Encoding { get; set; }

        public string Content { get; set; }

        public Stream StreamContent { get; set; }

        /// <summary>
        /// millisecond
        /// </summary>
        public int Elapsed { get; set; }

        public Type DownloaderType { get; set; }

        public Exception Exception { get; set; }

        public static Response Error(Request req, string message, Type downloaderType, Exception ex)
        {
            var res = new Response()
            {
                Request = req,
                HttpStatusCode = HttpStatusCode.InternalServerError,
                IsSuccessCode = false,
                ReasonPhrase = message,
                DownloaderType = downloaderType,
                Exception = ex
            };
            return res;
        }
    }
}
