using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Plunder.Setting.Core.Http
{
    public class HtmlActionResult : IHttpActionResult
    {
        private const string ViewDirectory = @"E:devConsoleApplication8ConsoleApplication8";
        private readonly string _view;
        private readonly dynamic _model;

        private readonly HttpRequestMessage _request;
        private readonly string _location;

        public HtmlActionResult(string viewName, object model)
        {
            _view = LoadView(viewName);
            _model = model;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static string LoadView(string name)
        {
            var view = File.ReadAllText(Path.Combine(ViewDirectory, name + ".cshtml"));
            return view;
        }
    }
}
