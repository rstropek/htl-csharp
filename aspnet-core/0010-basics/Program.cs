using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AspNetCoreBasics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8080")   // Listen on port 8080
                .UseStartup<Startup>()      // Web app code in class `Startup`
                .Build()
                .Run();
        }
    }
}
