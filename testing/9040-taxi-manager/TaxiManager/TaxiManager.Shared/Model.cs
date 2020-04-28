using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TaxiManager.Shared
{
    public class Taxi
    {
        public int ID { get; set; }
        public string LicensePlate { get; set; }
        [JsonIgnore]
        public List<TaxiRide> Rides { get; set; }
    }

    public class Driver
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<TaxiRide> Rides { get; set; }
    }

    public class TaxiRide
    {
        public int ID { get; set; }
        public Taxi Taxi { get; set; }
        public Driver Driver { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public decimal? Charge { get; set; }
    }

    public class StartRideDto
    {
        public int TaxiID { get; set; }
        public int DriverID { get; set; }
    }

    public class EndRideDto
    {
        public decimal Charge { get; set; }
    }

    public class DriverStatistics
    {
        public string DriverName { get; set; }
        public decimal TotalCharge { get; set; }
    }
}
