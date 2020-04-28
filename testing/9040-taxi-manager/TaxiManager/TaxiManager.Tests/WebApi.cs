using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxiManager.Shared;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    [TestClass]
    public class WebApi : WebApiTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context) => Setup();

        [ClassCleanup]
        public static void Cleanup() => Clean();

        [TestMethod]
        [Description("Verifies that rides can be started correctly (2 Points)")]
        public async Task StartRide()
        {
            var newTaxi = await DbContext.AddDummyTaxiAsync();
            var newDriver = await DbContext.AddDummyDriverAsync();
            var startRide = new StartRideDto
            {
                TaxiID = newTaxi.ID,
                DriverID = newDriver.ID
            };

            var body = new StringContent(JsonConvert.SerializeObject(startRide), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/rides", body);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newRide = JsonConvert.DeserializeObject<TaxiRide>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(newTaxi.Taxi.LicensePlate, newRide.Taxi.LicensePlate);
            Assert.AreEqual(newTaxi.ID, newRide.Taxi.ID);
            Assert.AreEqual(newDriver.Driver.Name, newRide.Driver.Name);
            Assert.AreEqual(newDriver.ID, newRide.Driver.ID);
            Assert.IsTrue(newRide.Start <= DateTime.Now && newRide.Start >= DateTime.Now.AddSeconds(-5));
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct error code if body is empty")]
        public async Task StartRideWithoutBody()
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/rides", body);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct error code if taxi ID is invalid (=unknown)")]
        public async Task StartRideInvalidTaxi()
        {
            var (_, newDriverID) = await DbContext.AddDummyDriverAsync();
            var startRide = new StartRideDto
            {
                TaxiID = Int32.MaxValue,
                DriverID = newDriverID
            };

            var body = new StringContent(JsonConvert.SerializeObject(startRide), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/rides", body);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct error code if driver ID is invalid (=unknown)")]
        public async Task StartRideInvalidDriver()
        {
            var (_, newTaxiID) = await DbContext.AddDummyTaxiAsync();
            var startRide = new StartRideDto
            {
                TaxiID = newTaxiID,
                DriverID = Int32.MaxValue
            };

            var body = new StringContent(JsonConvert.SerializeObject(startRide), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/rides", body);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        [Description("Verifies that rides can be ended correctly (2 Points)")]
        public async Task EndRide()
        {
            const decimal charge = 99m;
            var (_, _, rideID) = await DbContext.AddDummyRideAsync();
            var endRide = new EndRideDto { Charge = charge };

            var body = new StringContent(JsonConvert.SerializeObject(endRide), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/api/rides/{rideID}", body);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var newRide = JsonConvert.DeserializeObject<TaxiRide>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(charge, newRide.Charge);
            Assert.IsTrue(newRide.End <= DateTime.Now && newRide.End >= DateTime.Now.AddSeconds(-5));
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct error code if body is empty")]
        public async Task EndRideWithoutBody()
        {
            var body = new StringContent("", Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/api/rides", body);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct error code if ride ID is invalid (=unknown)")]
        public async Task EndRideInvalidID()
        {
            var startRide = new EndRideDto { Charge = 0m };
            var body = new StringContent(JsonConvert.SerializeObject(startRide), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync($"/api/rides/{Int32.MaxValue}", body);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct ongoing rides")]
        public async Task OngoingRides()
        {
            await DbContext.ClearAsync();
            await DbContext.AddDummyRideAsync();
            var response = await Client.GetAsync("/api/rides/ongoing");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var rides = JsonConvert.DeserializeObject<List<TaxiRide>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(1, rides.Count);
        }

        [TestMethod]
        [Description("Verifies that the web api returns the correct completed rides")]
        public async Task CompletedRides()
        {
            await DbContext.ClearAsync();
            var (_, _, rideID) = await DbContext.AddDummyRideAsync();
            await DbContext.EndRideAsync(rideID, 99m);
            var response = await Client.GetAsync("/api/rides/completed");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var rides = JsonConvert.DeserializeObject<List<TaxiRide>>(await response.Content.ReadAsStringAsync());
            Assert.AreEqual(1, rides.Count);
        }
    }
}
