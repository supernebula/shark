using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Plunder.Setting.Core.Http;

namespace Plunder.Setting
{
    public partial class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            //ConfigStaticFile(app);

        var builder = new ContainerBuilder();
            // STANDARD WEB API SETUP:

            // Get your HttpConfiguration. In OWIN, you'll create one
            // rather than using GlobalConfiguration.
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Default_Api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Default_UI",
                routeTemplate: "ui/{controller}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );


            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.Register

            // Run other optional steps, like registering filters,
            // per-controller-type services, etc., then set the dependency resolver
            // to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // OWIN WEB API SETUP:

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            Console.ForegroundColor = ConsoleColor.Green;
            app.UseWebApi(config);
            app.Use((context, next) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(context.Request.Uri.AbsoluteUri);
                return next.Invoke();
            }); ;
            config.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API")).EnableSwaggerUi();
        }
    }

}
