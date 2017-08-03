using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Diagnostics;
using Owin;
using Microsoft.Owin.Builder;

namespace Plunder.WebHost
{
    public class ConsoleStartup
    {
        public void Configuration(AppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "Default", id = RouteParameter.Optional }
                );

            ////appBuilder.UseWebApi(config);
            appBuilder.UseWelcomePage(new WelcomePageOptions());

            appBuilder.Run(context =>
            {
                if (!String.IsNullOrWhiteSpace(context.Request.Path.Value) && context.Request.Path.Value.StartsWith("/fail"))
                {
                    throw new Exception("Random exception");
                }

                context.Response.ContentType = "text/plan";
                return context.Response.WriteAsync("Hello world.");
            });
        }
    }
}
