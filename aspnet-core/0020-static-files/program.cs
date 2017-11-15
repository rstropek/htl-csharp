using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;

namespace myApp
{
    public class Program
    {
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
    }
}
