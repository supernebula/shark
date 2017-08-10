using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using RazorEngine;
using RazorEngine.Templating;
using System.Web.Http.Controllers;


namespace Plunder.Setting.Core.Http
{
    public class HtmlActionResult<T> : IHttpActionResult
    {
        private const string ViewDirectory = @"E:devConsoleApplication8ConsoleApplication8";
        private readonly string _view;
        private readonly T _model;
        private readonly HttpControllerContext _controllerContext;
        private readonly string _templeteKey;

        public readonly string Controller;
        public readonly string Action;


        private readonly HttpRequestMessage _request;
        private readonly string _location;

        //public object HostingEnvironment { get; private set; }

        public HtmlActionResult(HttpControllerContext controllerContext, string viewName, T model)
        {
            _controllerContext = controllerContext;
            Controller = _controllerContext.ControllerDescriptor.ControllerName;
            Action = _controllerContext.Request.GetActionDescriptor().ActionName;
            _view = LoadView(viewName);
            _model = model;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            //RazorEngine.Engine.Razor.Compile(_view, _model);
            //response.Content = new StringContent(parsedView);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return Task.FromResult(response);
        }

        private static string LoadView(string name)
        {
            var view = File.ReadAllText(Path.Combine(ViewDirectory, name + ".cshtml"));
            return view;
        }

        private string ViewPath(string controllerName, string actionName, string viewName)
        {
            if (!viewName.ToLower().EndsWith(".cshtml"))
                viewName += ".cshtml";
            var relatePath = $"/{controllerName}/{actionName}/{viewName}";
            return relatePath;
            //var dir = HostingEnvironment.MapPath("~/");
        }
    }
}
