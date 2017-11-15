using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace myApp { 
    public class Startup {
        public void Configure(IApplicationBuilder app, ILoggerFactory log)
        {
            app.UseDefaultFiles(new DefaultFilesOptions {
                DefaultFileNames = new[] { "index.html" }
            });
            app.UseStaticFiles();

            // Just for demo purposes we add a second middleware after static files.
            app.Map("/helloworld", subApp => subApp.Run(
                async context => await context.Response.WriteAsync("Hello world!")));
        }
    }
}
