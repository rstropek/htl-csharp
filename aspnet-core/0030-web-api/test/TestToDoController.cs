using AspNetCoreBasics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace test
{
    [TestClass]
    public partial class TestToDoController
    {
        private HttpClient client;

        public TestToDoController()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            client = testServer.CreateClient();
        }
    }
}
