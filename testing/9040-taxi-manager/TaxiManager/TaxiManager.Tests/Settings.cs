using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    [TestClass]
    public class Settings
    {
        public static string DatabaseConnectionString { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            DatabaseConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
    }
}
