using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using TaxiManager.WebApi.Data;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXAM!
 */

namespace TaxiManager.Tests
{
    public abstract class WebApiTestBase
    {
        protected static TestServer Server { get; set; }
        protected static HttpClient Client { get; set; }
        protected static TaxiDataContext DbContext { get; set; }

        protected static void Setup()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<StartupMock>());
            Client = Server.CreateClient();

            var builder = new DbContextOptionsBuilder<TaxiDataContext>();
            builder.UseSqlServer(Settings.DatabaseConnectionString);
            DbContext = new TaxiDataContext(builder.Options);
        }

        protected static void Clean()
        {
            Client.Dispose();
            Server.Dispose();
            DbContext.Dispose();
        }
    }
}