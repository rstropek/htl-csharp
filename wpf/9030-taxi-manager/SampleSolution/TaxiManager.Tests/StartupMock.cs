using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiManager.WebApi.Data;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXAM!
 */

namespace TaxiManager.Tests
{
    public class StartupMock
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaxiDataContext>(options => options.UseSqlServer(Settings.DatabaseConnectionString));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }

}
