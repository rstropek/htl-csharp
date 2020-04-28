using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    [TestClass]
    public class DatabaseStructure
    {
        private static SqlConnection DbConnection { get; set; }

        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            DbConnection = new SqlConnection(Settings.DatabaseConnectionString);
            await DbConnection.OpenAsync();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DbConnection.Dispose();
        }

        [TestMethod]
        [Description("Verifies that tables have been created in the DB with correct names")]
        public async Task TestTablesExist()
        {
            var tableNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS TableName FROM sys.tables WHERE name IN ('Taxis', 'Drivers', 'Rides')");
            CollectionAssert.AreEquivalent(new[] { "Taxis", "Drivers", "Rides" }, tableNames.ToList());
        }

        [TestMethod]
        [Description("Verifies that tables have correct columns")]
        public async Task TestCorrectColumns()
        {
            var columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Taxis')");
            CollectionAssert.AreEquivalent(new[] { "ID", "LicensePlate" }, columnNames.ToList());

            columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Drivers')");
            CollectionAssert.AreEquivalent(new[] { "ID", "Name" }, columnNames.ToList());

            columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Rides')");
            CollectionAssert.AreEquivalent(new[] { "ID", "Charge", "DriverID", "End", "Start", "TaxiID" }, columnNames.ToList());
        }

        [TestMethod]
        [Description("Verifies that columns are not nullable")]
        public async Task TestNotNullColumns()
        {
            Assert.IsNull(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Taxis') AND is_nullable = 1"));
            Assert.IsNull(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Drivers') AND is_nullable = 1"));
            Assert.IsNull(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rides') AND name IN ('ID', 'Start') AND is_nullable = 1"));
        }

        [TestMethod]
        [Description("Verifies that relations are not nullable")]
        public async Task TestNotNullRelations()
        {
            Assert.IsNull(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rides') AND name IN ('TaxiID', 'DriverID') AND is_nullable = 1"));
        }
    }
}
