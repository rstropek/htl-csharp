using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    [TestClass]
    public class DataContextMethods
    {
        private static TaxiDataContext DbContext { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            var builder = new DbContextOptionsBuilder<TaxiDataContext>();
            builder.UseSqlServer(Settings.DatabaseConnectionString);
            DbContext = new TaxiDataContext(builder.Options);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            DbContext.Dispose();
        }

        [TestMethod]
        [Description("Verifies that taxis are created correctly")]
        public async Task AddTaxi()
        {
            var (newTaxi, _) = await DbContext.AddDummyTaxiAsync();

            Assert.IsNotNull(await DbContext.Taxis.FirstAsync(t => t.LicensePlate == newTaxi.LicensePlate));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Verifies that an exception is thrown if argument is null")]
        public async Task AddTaxiArgumentNull()
        {
            await DbContext.AddTaxiAsync(null);
        }

        [TestMethod]
        [Description("Verifies that the ID of the created record is returned correctly")]
        public async Task AddTaxiReturnsValidID()
        {
            var newTaxi = await DbContext.AddDummyTaxiAsync();
            var insertedTaxi = await DbContext.Taxis.FirstAsync(t => t.ID == newTaxi.ID);

            Assert.IsNotNull(insertedTaxi);
            Assert.AreEqual(newTaxi.Taxi.LicensePlate, insertedTaxi.LicensePlate);
        }

        [TestMethod]
        [Description("Verifies that drivers are created correctly")]
        public async Task AddDriver()
        {
            var newDriver = await DbContext.AddDummyDriverAsync();
            Assert.IsNotNull(await DbContext.Drivers.FirstAsync(d => d.Name == newDriver.Driver.Name));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Verifies that an exception is thrown if argument is null")]
        public async Task AddDriverArgumentNull()
        {
            await DbContext.AddDriverAsync(null);
        }

        [TestMethod]
        [Description("Verifies that the ID of the created record is returned correctly")]
        public async Task AddDriverReturnsValidID()
        {
            var newDriver = await DbContext.AddDummyDriverAsync();
            var insertedDriver = await DbContext.Drivers.FirstAsync(d => d.ID == newDriver.ID);

            Assert.IsNotNull(insertedDriver);
            Assert.AreEqual(newDriver.Driver.Name, insertedDriver.Name);
        }

        [TestMethod]
        [Description("Verifies that taxi rides can be started correctly")]
        public async Task StartRide()
        {
            var (taxi, driver, _) = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.Driver.Name == driver.Name && r.Taxi.LicensePlate == taxi.LicensePlate);

            Assert.IsNotNull(newRide);
            Assert.AreEqual(taxi, newRide.Taxi);
            Assert.AreEqual(driver, newRide.Driver);
            Assert.IsTrue(newRide.Start <= DateTime.Now && newRide.Start >= DateTime.Now.AddSeconds(-5));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Verifies that an exception is thrown if argument is null")]
        public async Task StartRideArgumentNull()
        {
            await DbContext.StartRideAsync(null, null);
        }

        [TestMethod]
        [Description("Verifies that the ID of the created record is returned correctly")]
        public async Task StartRideReturnsValidID()
        {
            var ride = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.ID == ride.ID);

            Assert.IsNotNull(newRide);
            Assert.AreEqual(ride.Taxi, newRide.Taxi);
            Assert.AreEqual(ride.Driver, newRide.Driver);
        }

        [TestMethod]
        [Description("Verifies that taxi rides can be ended correctly")]
        public async Task EndRide()
        {
            const decimal charge = 99m;
            var (taxi, driver, _) = await DbContext.AddDummyRideAsync();
            var newRide = await DbContext.Rides.FirstAsync(r => r.Driver.Name == driver.Name && r.Taxi.LicensePlate == taxi.LicensePlate);

            await DbContext.EndRideAsync(newRide.ID, charge);

            newRide = await DbContext.Rides.FirstAsync(r => r.ID == newRide.ID);
            Assert.IsTrue(newRide.End <= DateTime.Now && newRide.End >= DateTime.Now.AddSeconds(-5));
            Assert.AreEqual(charge, newRide.Charge);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [Description("Verifies that an exception is thrown if argument is invalid")]
        public async Task EndRideNegativeCharge()
        {
            await DbContext.EndRideAsync(Int32.MaxValue, -1m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Description("Verifies that an exception is thrown if invalid ride ID is provided")]
        public async Task EndRideInvalidID()
        {
            await DbContext.EndRideAsync(Int32.MaxValue, 1m);
        }

        [TestMethod]
        [Description("Verifies that clear deletes all data from the database")]
        public async Task Clear()
        {
            await DbContext.AddDummyRideAsync();
            await DbContext.ClearAsync();

            Assert.AreEqual(0, await DbContext.Taxis.CountAsync());
            Assert.AreEqual(0, await DbContext.Drivers.CountAsync());
            Assert.AreEqual(0, await DbContext.Rides.CountAsync());
        }

        private class DriverStatisticsComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is DriverStatistics ds1 && y is DriverStatistics ds2)
                {
                    return ds1.DriverName == ds2.DriverName && ds1.TotalCharge == ds2.TotalCharge ? 0 : 1;
                }

                return -1;
            }
        }

        [TestMethod]
        [Description("Verifies that the driver statistic is calculated correctly (2 Points)")]
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
            CollectionAssert.AreEqual(expected, actual, new DriverStatisticsComparer());
        }
    }
}
