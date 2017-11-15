using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreBasics {
    public class Startup {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.Use(async (context, next) => {
                if (context.Request.Cookies.ContainsKey("some-cookie")) {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("some-cookie not supported");
                } else { await next(); }
            });
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Hello ");
                await next();
            });
            app.Run(async (context) => { await context.Response.WriteAsync("World!"); });
        }
    }
}
