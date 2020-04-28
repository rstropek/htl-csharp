using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;
using Xunit;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    public class DataContextMethods : IClassFixture<DataContextFixture>
    {
        private TaxiDataContext DbContext { get; }

        public DataContextMethods(DataContextFixture fixture)
        {
            DbContext = fixture.DbContext;
        }

        [Fact(DisplayName = "Verifies that taxis are created correctly")]
        public async Task AddTaxi()
        {
            var (newTaxi, _) = await DbContext.AddDummyTaxiAsync();

            Assert.NotNull(await DbContext.Taxis.FirstAsync(t => t.LicensePlate == newTaxi.LicensePlate));
        }

        [Fact(DisplayName = "Verifies that an exception is thrown if argument is null")]
        public async Task AddTaxiArgumentNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await DbContext.AddTaxiAsync(null));
        }

        [Fact(DisplayName = "Verifies that the ID of the created record is returned correctly")]
        public async Task AddTaxiReturnsValidID()
        {
            var newTaxi = await DbContext.AddDummyTaxiAsync();
            var insertedTaxi = await DbContext.Taxis.FirstAsync(t => t.ID == newTaxi.ID);

            Assert.NotNull(insertedTaxi);
            Assert.Equal(newTaxi.Taxi.LicensePlate, insertedTaxi.LicensePlate);
        }

        [Fact(DisplayName = "Verifies that drivers are created correctly")]
        public async Task AddDriver()
        {
            var newDriver = await DbContext.AddDummyDriverAsync();
            Assert.NotNull(await DbContext.Drivers.FirstAsync(d => d.Name == newDriver.Driver.Name));
        }

        [Fact(DisplayName = "Verifies that an exception is thrown if argument is null")]
        public async Task AddDriverArgumentNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await DbContext.AddDriverAsync(null));
        }

        [Fact(DisplayName = "Verifies that the ID of the created record is returned correctly")]
        public async Task AddDriverReturnsValidID()
        {
            var newDriver = await DbContext.AddDummyDriverAsync();
            var insertedDriver = await DbContext.Drivers.FirstAsync(d => d.ID == newDriver.ID);

            Assert.NotNull(insertedDriver);
            Assert.Equal(newDriver.Driver.Name, insertedDriver.Name);
        }

        [Fact(DisplayName = "Verifies that taxi rides can be started correctly")]
        public async Task StartRide()
        {
            var (taxi, driver, _) = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.Driver.Name == driver.Name && r.Taxi.LicensePlate == taxi.LicensePlate);

            Assert.NotNull(newRide);
            Assert.Equal(taxi, newRide.Taxi);
            Assert.Equal(driver, newRide.Driver);
            Assert.True(newRide.Start <= DateTime.Now && newRide.Start >= DateTime.Now.AddSeconds(-5));
        }

        [Fact(DisplayName = "Verifies that an exception is thrown if argument is null")]
        public async Task StartRideArgumentNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await DbContext.StartRideAsync(null, null));
        }

        [Fact(DisplayName = "Verifies that the ID of the created record is returned correctly")]
        public async Task StartRideReturnsValidID()
        {
            var ride = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.ID == ride.ID);

            Assert.NotNull(newRide);
            Assert.Equal(ride.Taxi, newRide.Taxi);
            Assert.Equal(ride.Driver, newRide.Driver);
        }

        [Fact(DisplayName = "Verifies that taxi rides can be ended correctly")]
        public async Task EndRide()
        {
            const decimal charge = 99m;
            var (taxi, driver, _) = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.Driver.Name == driver.Name && r.Taxi.LicensePlate == taxi.LicensePlate);

            await DbContext.EndRideAsync(newRide.ID, charge);

            newRide = await DbContext.Rides.FirstAsync(r => r.ID == newRide.ID);
            Assert.True(newRide.End <= DateTime.Now && newRide.End >= DateTime.Now.AddSeconds(-5));
            Assert.Equal(charge, newRide.Charge);
        }

        [Fact(DisplayName = "Verifies that an exception is thrown if argument is invalid")]
        public async Task EndRideNegativeCharge()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await DbContext.EndRideAsync(Int32.MaxValue, -1m));
        }

        [Fact(DisplayName = "Verifies that an exception is thrown if invalid ride ID is provided")]
        public async Task EndRideInvalidID()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await DbContext.EndRideAsync(Int32.MaxValue, 1m));
        }

        [Fact(DisplayName = "Verifies that clear deletes all data from the database")]
        public async Task Clear()
        {
            await DbContext.AddDummyRideAsync();
            await DbContext.ClearAsync();

            Assert.Equal(0, await DbContext.Taxis.CountAsync());
            Assert.Equal(0, await DbContext.Drivers.CountAsync());
            Assert.Equal(0, await DbContext.Rides.CountAsync());
        }

        private class DriverStatisticsComparer : IEqualityComparer<DriverStatistics>
        {
            public bool Equals([AllowNull] DriverStatistics ds1, [AllowNull] DriverStatistics ds2)
            {
                return ds1.DriverName == ds2.DriverName && ds1.TotalCharge == ds2.TotalCharge;
            }

            public int GetHashCode([DisallowNull] DriverStatistics ds)
            {
                return ds.GetHashCode();
            }
        }

        [Fact(DisplayName = "Verifies that the driver statistic is calculated correctly (2 Points)")]
        public async Task GetDriverStatistics()
        {
            const decimal charge = 99m;
            await DbContext.ClearAsync();
            var (taxi, _) = await DbContext.AddDummyTaxiAsync();
            var (driver, _) = await DbContext.AddDummyDriverAsync();
            for (var i = 0; i < 5; i++)
            {
                var newRideID = await DbContext.StartRideAsync(taxi, driver);
                await DbContext.EndRideAsync(newRideID, charge);
            }

            var expected = new[] { new DriverStatistics { DriverName = driver.Name, TotalCharge = charge * 5 } };
            var actual = await DbContext.GetDriverStatisticsAsync(DateTime.Today.Year, DateTime.Today.Month);
            Assert.Equal(expected, actual, new DriverStatisticsComparer());
        }
    }
}
