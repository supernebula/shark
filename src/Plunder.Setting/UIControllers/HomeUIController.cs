using System.Net.Http;
using System.Web.Http;
using RazEngine = RazorEngine.Engine;
using RazorEngine.Templating;
using System.Net;
using System.Net.Http.Headers;

namespace Plunder.Setting.UIControllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("ui/Home")]
    public class HomeUIController : ApiController
    {
        [Route("Index")]
        [Route("~/Home")]
        [Route("~/ui")]
        [Route("~/")]
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var templateKey = "domainui.index";
            string template = "Hello @Model ! This is UI! Path:" + Request.RequestUri.AbsolutePath;
            //string result = RazEngine.Razor.Compile(template, new { Name = "World" });
            string result = null;
            if (RazEngine.Razor.IsTemplateCached(templateKey, typeof(string)))
                result = RazEngine.Razor.Run(templateKey, model: "Man");
            else
                result = RazEngine.Razor.RunCompile(template, "templateKey", null, "Man");

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            httpResponseMessage.Content = new StringContent(result);

            MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("text/html");

            mediaTypeHeaderValue.CharSet = System.Text.Encoding.UTF8.WebName;

            httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;

            return httpResponseMessage;
        }
    }
}
