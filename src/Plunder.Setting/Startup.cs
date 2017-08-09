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
            ConfigStaticFile(app);

            //var relativePath = string.Format(@"..{0}..{0}", Path.DirectorySeparatorChar);
            //string contentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);

            //app.UseFileServer(new FileServerOptions()
            //{
            //    RequestPath = PathString.Empty,
            //    FileSystem = new PhysicalFileSystem(Path.Combine(contentPath, @"wwwroot")),
            //});

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    RequestPath = new PathString("/wwwroot"),
            //    FileSystem = new PhysicalFileSystem(Path.Combine(contentPath, @"wwwroot"))
            //});
       


        var builder = new ContainerBuilder();
            // STANDARD WEB API SETUP:

            // Get your HttpConfiguration. In OWIN, you'll create one
            // rather than using GlobalConfiguration.
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );//.DataTokens["namespace"] = new string[] { typeof(Plunder.Setting.ApiControllers.DomainController).Namespace };

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi2",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    namespaces: new string[] { typeof(Plunder.Setting.ApiControllers.DomainController).Namespace }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultUI",
                routeTemplate: "ui/{controller}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );//.DataTokens["namespace"] = new string[] { typeof(Plunder.Setting.Controllers.DomainController).Namespace };


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
