using Microsoft.EntityFrameworkCore;
using System;
using TaxiManager.WebApi.Data;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    public class DataContextFixture : IDisposable
    {
        public TaxiDataContext DbContext { get; set; }

        public DataContextFixture()
        {
            var builder = new DbContextOptionsBuilder<TaxiDataContext>();
            builder.UseSqlServer(new Settings().GetConnectionString());
            DbContext = new TaxiDataContext(builder.Options);

            // Make sure that all migrations have been applied.
            DbContext.Database.Migrate();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
