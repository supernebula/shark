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

namespace Plunder.Setting.Controllers
{
    public class DomainViewController : ApiController
    {
        [HttpGet]
        public HttpContent Index()
        {
            string template = "Hello @Model.Name! Welcome to Web API and Razor!";
            //string result = RazEngine.Razor.Compile(template, new { Name = "World" });
            var result = RazEngine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            return new StringContent(result, System.Text.Encoding.UTF8, "text/html");
        }
    }
}
