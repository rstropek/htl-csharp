using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiManager.Shared;

namespace TaxiManager.WebApi.Data
{
    public class TaxiDataContext : DbContext
    {
        public DbSet<Taxi> Taxis { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<TaxiRide> Rides { get; set; }

        public TaxiDataContext(DbContextOptions<TaxiDataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Taxi>().Property(t => t.LicensePlate).IsRequired();
            modelBuilder.Entity<Driver>().Property(t => t.Name).IsRequired();
            modelBuilder.Entity<TaxiRide>().Property(t => t.Start).IsRequired();
            modelBuilder.Entity<TaxiRide>().HasOne(t => t.Taxi).WithMany(t => t.Rides).IsRequired();
            modelBuilder.Entity<TaxiRide>().HasOne(t => t.Driver).WithMany(t => t.Rides).IsRequired();
        }

        public async Task ClearAsync()
        {
            Taxis.RemoveRange(Taxis.ToArray());
            Drivers.RemoveRange(Drivers.ToArray());
            Rides.RemoveRange(Rides.ToArray());
            await SaveChangesAsync();
        }

        public async Task<int> AddTaxiAsync(Taxi newTaxi)
        {
            if (newTaxi == null)
            {
                throw new ArgumentNullException(nameof(newTaxi));
            }

            Taxis.Add(newTaxi);
            await SaveChangesAsync();
            return newTaxi.ID;
        }

        public async Task<int> AddDriverAsync(Driver newDriver)
        {
            if (newDriver == null)
            {
                throw new ArgumentNullException(nameof(newDriver));
            }

            Drivers.Add(newDriver);
            await SaveChangesAsync();
            return newDriver.ID;
        }

        public async Task<int> StartRideAsync(Taxi taxi, Driver driver)
        {
            if (taxi == null)
            {
                throw new ArgumentNullException(nameof(taxi));
            }

            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var newRide = new TaxiRide
            {
                Start = DateTime.Now,
                Taxi = taxi,
                Driver = driver
            };
            Rides.Add(newRide);
            await SaveChangesAsync();
            return newRide.ID;
        }

        public async Task EndRideAsync(int rideID, decimal charge)
        {
            if (charge < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(charge));
            }

            if (!await Rides.AnyAsync(r => r.ID == rideID))
            {
                throw new ArgumentException(nameof(rideID));
            }

            var ride = await Rides.FirstAsync(r => r.ID == rideID);
            ride.End = DateTime.Now;
            ride.Charge = charge;
            await SaveChangesAsync();
        }

        public async Task<List<DriverStatistics>> GetDriverStatisticsAsync(int year, int month)
        {
            return await Rides//.Where(r => r.Start.Year == year && r.Start.Month == month && r.Charge.HasValue)
                .GroupBy(r => r.Driver.Name)
                .Select(r => new DriverStatistics
                {
                    DriverName = r.Key,
                    TotalCharge = r.Sum(x => x.Charge.Value)
                })
                .ToListAsync();
        }
    }
}
