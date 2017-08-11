using System.Net.Http;
using System.Web.Http;
using RazEngine = RazorEngine.Engine;
using RazorEngine.Templating;
using System.Net;
using System.Net.Http.Headers;

namespace Plunder.Setting.UIControllers
{
    [RoutePrefix("ui/Domain")]
    public class DomainUIController : ApiController
    {
        [HttpGet]
        [Route("Index")]
        public HttpResponseMessage Index()
        {
            string template = "Hello @Model.Name! This is UI!";
            //string result = RazEngine.Razor.Compile(template, new { Name = "World" });
            var result = RazEngine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            httpResponseMessage.Content = new StringContent(result);

            MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("text/html");

            mediaTypeHeaderValue.CharSet = System.Text.Encoding.UTF8.WebName;

            httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;

            return httpResponseMessage;
        }
    }
}
