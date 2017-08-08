using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plunder.Setting
{
    /// <summary>
    /// <see cref="https://aspnet.codeplex.com/SourceControl/latest#Samples/Katana/StaticFilesSample/Startup.cs"/>
    /// <see cref="http://dotnet.today/en/aspnet5-vnext/fundamentals/static-files.html"/>
    /// </summary>
    public partial class Startup
    {
        public void ConfigStaticFile(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif

            var relativePath = string.Format(@"..{0}..{0}", Path.DirectorySeparatorChar);
            //string contentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), relativePath);

            // Remap '/' to '.\wwwroot\'.
            // Turns on static files and default files.

            var fileServOptions = new FileServerOptions()
            {
                RequestPath = PathString.Empty,
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileSystem = new PhysicalFileSystem(Path.Combine(relativePath, @".\wwwroot"))
            };
            fileServOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseFileServer(fileServOptions);

            app.UseDefaultFiles();
            //// Only serve files requested by name.
            //app.UseStaticFiles("/wwwroot");
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = new PathString("/wwwroot"),
                FileSystem = new PhysicalFileSystem(Path.Combine(relativePath, @"wwwroot"))
            });

            //// Turns on static files, directory browsing, and default files.
            //app.UseFileServer(new FileServerOptions()
            //{
            //    RequestPath = new PathString("/wwwroot"),
            //    EnableDirectoryBrowsing = true,
            //});

            //// Browse the root of your application (but do not serve the files).
            //// NOTE: Avoid serving static files from the root of your application or bin folder,
            //// it allows people to download your application binaries, config files, etc..
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    RequestPath = new PathString("/src"),
            //    FileSystem = new PhysicalFileSystem(@""),
            //});

            // Anything not handled will land at the welcome page.
            app.UseWelcomePage("/index.html");
        }
    }
}



//string staticFilesDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wwwroot");
//app.UseStaticFiles(staticFilesDir);