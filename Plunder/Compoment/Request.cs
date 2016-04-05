
namespace Plunder.Compoment
{
    public class Request
    {
        public Site Site { get; set; }

        public string Topic { get; set; }


        public string Uri { get; set; }

        public string Method { get; set; }

        public string Priority { get; set; }

        public int AllowedRetryCount { get; set; }

        public bool UseProxy { get; set; }

        public string Downloader { get; set; }
    }
}
