using System;
using System.Data.SqlClient;
using Xunit;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

// This example contains integration tests with a database. To simplify
// testing code, parallel test execution is disabled. In practice, disable
// parallel tests only for DB-related test collections.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace TaxiManager.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public SqlConnection DbConnection { get; }

        public DatabaseFixture()
        {
            DbConnection = new SqlConnection(new Settings().GetConnectionString());
            DbConnection.Open();
        }

        public void Dispose()
        {
            DbConnection.Dispose();
        }
    }
}
