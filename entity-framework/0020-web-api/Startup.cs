using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkWebApi.Models;
using Newtonsoft.Json;
using AutoMapper;

namespace EntityFrameworkWebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // Configure JSON serialization so that it ignores loops
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // We use SQL Server Express LocalDB for this example
            const string connection = @"Server=(localdb)\dev;Database=AddressBook;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<AddressBookContext>(options => options.UseSqlServer(connection));

            // Configure AutoMapper, a very useful component for copying data
            // between objects. For details see http://automapper.org/
            var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
