using AspNetMvcDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetMvcDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Note how we add a singleton class acting as the repository for heroes.
            // This is called "Dependency Injection". Learn more at 
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
            services.AddSingleton<IHeroRepository>(new HeroRepository());

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add status code page showing errors like "Not Found"
            app.UseStatusCodePages();

            // Make "Heroes" the default route. Learn more about routing at
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing
            app.UseMvc(routes => routes.MapRoute(
                name: "default",
                template: "{controller=Heroes}/{action=Index}/{id?}"));
        }
    }
}
