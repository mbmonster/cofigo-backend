using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.IO;
using Microsoft.Owin.FileSystems;

[assembly: OwinStartup(typeof(LastTest.Startup))]

namespace LastTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureStaticFiles(app);
           
        }
        private void ConfigureStaticFiles(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Uses static file for the current path.

            string root = AppDomain.CurrentDomain.BaseDirectory;
            string wwwroot = Path.Combine(root, "wwwroot");


            var fileServerOptions = new FileServerOptions()
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem(wwwroot)
            };

            string rootD = AppDomain.CurrentDomain.BaseDirectory;
         


            var fileServerOptions2 = new FileServerOptions()
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem(rootD)
            };

            fileServerOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            
            app.UseFileServer(fileServerOptions);
            app.UseFileServer(fileServerOptions2);
        }
    }
}
