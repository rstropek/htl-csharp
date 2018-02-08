using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Evaluate.Logic;

namespace Evaluate
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Optional exercise: Use ASP.NET Core Dependency Injection
            services.AddSingleton<IExpressionEvaluator>(new ExpressionEvaluator());

            // Required exercise: Add ASP.NET MVC to application
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Required exercise: Add ASP.NET MVC to application
            app.UseMvc();
        }
    }
}
