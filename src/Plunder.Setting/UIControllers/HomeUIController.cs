using System.Net.Http;
using System.Web.Http;
using RazEngine = RazorEngine.Engine;
using RazorEngine.Templating;
using System.Net;
using System.Net.Http.Headers;

namespace Plunder.Setting.UIControllers
{
    /// <summary>
    /// <see cref="https://www.strathweb.com/2013/06/ihttpactionresult-new-way-of-creating-responses-in-asp-net-web-api-2/"/>
    /// </summary>
    [RoutePrefix("ui/Home")]
    public class HomeUIController : ApiController
    {
        [Route("Index")]
        [Route("~/Home")]
        [Route("~/ui")]
        [Route("~/")]
        [HttpGet]
        public IHttpActionResult Index()
        {
            var route = this.ControllerContext.RequestContext.RouteData;

            Microsoft.Owin.FileSystems.PhysicalFileSystem 

            return null;

            //var templateKey = "domainui.index";
            //string template = "Hello @Model ! This is UI! Path:" + Request.RequestUri.AbsolutePath;
            ////string result = RazEngine.Razor.Compile(template, new { Name = "World" });
            //string result = null;
            //if (RazEngine.Razor.IsTemplateCached(templateKey, typeof(string)))
            //    result = RazEngine.Razor.Run(templateKey, model: "Man");
            //else
            //    result = RazEngine.Razor.RunCompile(template, "templateKey", null, "Man");


            //var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            //var viewPath = this.RequestContext.

            //httpResponseMessage.Content = new StringContent(result);


            //MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("text/html");

            //mediaTypeHeaderValue.CharSet = System.Text.Encoding.UTF8.WebName;

            //httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;

            //return httpResponseMessage;
        }


        public HttpResponseMessage Index2()
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
