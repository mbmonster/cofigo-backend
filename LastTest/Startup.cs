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
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string wwwroot = Path.Combine(root, "wwwroot");

            var fileServerOptions = new FileServerOptions()
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem(wwwroot)
            };

            fileServerOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(fileServerOptions);
        }
    }
}
