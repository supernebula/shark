using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using RazEngine = RazorEngine.Engine;
using RazorEngine.Compilation;
using RazorEngine.Templating;
using RazorEngine.Text;
using RazorEngine.Configuration;
using System.Net;
using System.Net.Http.Headers;

namespace Plunder.Setting.Controllers
{
    public class Domain2Controller : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            string template = "Hello @Model.Name! Welcome to Web API and Razor!";
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
