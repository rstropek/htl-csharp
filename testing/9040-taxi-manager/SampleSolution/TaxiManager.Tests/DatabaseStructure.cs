using Dapper;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    public class DatabaseStructure : IClassFixture<DatabaseFixture>
    {
        private SqlConnection DbConnection { get; }

        public DatabaseStructure(DatabaseFixture fixture)
        {
            DbConnection = fixture.DbConnection;
        }

        [Fact(DisplayName = "Verifies that tables have been created in the DB with correct names")]
        public async Task TestTablesExist()
        {
            var tableNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS TableName FROM sys.tables WHERE name IN ('Taxis', 'Drivers', 'Rides')");
            Assert.Equal(new[] { "Taxis", "Drivers", "Rides" }.OrderBy(x => x), tableNames.ToList().OrderBy(x => x));
        }

        [Fact(DisplayName = "Verifies that tables have correct columns")]
        public async Task TestCorrectColumns()
        {
            var columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Taxis')");
            Assert.Equal(new[] { "ID", "LicensePlate" }.OrderBy(x => x), columnNames.ToList().OrderBy(x => x));

            columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Drivers')");
            Assert.Equal(new[] { "ID", "Name" }.OrderBy(x => x), columnNames.ToList().OrderBy(x => x));

            columnNames = await DbConnection.QueryAsync<string>(
                "SELECT name AS ColumnName FROM sys.columns WHERE object_id = OBJECT_ID('Rides')");
            Assert.Equal(new[] { "ID", "Charge", "DriverID", "End", "Start", "TaxiID" }.OrderBy(x => x), columnNames.ToList().OrderBy(x => x));
        }

        [Fact(DisplayName = "Verifies that columns are not nullable")]
        public async Task TestNotNullColumns()
        {
            Assert.Null(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Taxis') AND is_nullable = 1"));
            Assert.Null(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Drivers') AND is_nullable = 1"));
            Assert.Null(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rides') AND name IN ('ID', 'Start') AND is_nullable = 1"));
        }

        [Fact(DisplayName = "Verifies that relations are not nullable")]
        public async Task TestNotNullRelations()
        {
            Assert.Null(await DbConnection.QueryFirstOrDefaultAsync<int?>(
                "SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Rides') AND name IN ('TaxiID', 'DriverID') AND is_nullable = 1"));
        }
    }
}
