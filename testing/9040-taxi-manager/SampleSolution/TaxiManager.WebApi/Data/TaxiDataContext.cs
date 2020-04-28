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
            modelBuilder.Entity<TaxiRide>().Property(t => t.Charge).HasColumnType("numeric(10,2)");
        }

        /// <summary>
        /// Deletes all rows from all tables (taxis, drivers, rides)
        /// </summary>
        public async Task ClearAsync()
        {
            Taxis.RemoveRange(Taxis.ToArray());
            Drivers.RemoveRange(Drivers.ToArray());
            Rides.RemoveRange(Rides.ToArray());
            await SaveChangesAsync();
        }

        /// <summary>
        /// Adds a taxi to the database
        /// </summary>
        /// <param name="newTaxi">Taxi data that should be added</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newTaxi"/> is null</exception>
        /// <returns>ID of the newly added taxi as created by the database</returns>
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

        /// <summary>
        /// Adds a driver to the database
        /// </summary>
        /// <param name="newDriver">Driver data that should be added</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="newDriver"/> is null</exception>
        /// <returns>ID of the newly added driver as created by the database</returns>
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

        /// <summary>
        /// Adds a new ongoing taxi ride to the database
        /// </summary>
        /// <param name="taxi">Taxi doing the ride</param>
        /// <param name="driver">Driver driving the taxi</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="taxi"/> or <paramref name="driver"/> are null</exception>
        /// <returns>ID of the newly added taxi ride as created by the database</returns>
        /// <remarks>
        /// The method sets <see cref="TaxiRide.Start"/> to the current system time (<see cref="DateTime.Now"/>).
        /// <see cref="TaxiRide.End"/> and <see cref="TaxiRide.Charge"/> are null. These values will be set
        /// when the taxi ride will be ended (see <see cref="EndRideAsync"/>.
        /// </remarks>
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

        /// <summary>
        /// Completes an existing ongoing taxi ride
        /// </summary>
        /// <param name="rideID">ID of the taxi ride that should be completed (previously returned by <see cref="StartRideAsync"/>)</param>
        /// <param name="charge">Charge of the taxi ride</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="charge"/> is negative</exception>
        /// <exception cref="ArgumentException">Thrown if no ride with ID <paramref name="rideID"/> exists</exception>
        /// <remarks>
        /// The method sets <see cref="TaxiRide.End"/> to the current system time (<see cref="DateTime.Now"/>)
        /// and stores <paramref name="charge"/> in <see cref="TaxiRide.Charge"/>.
        /// </remarks>
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

        /// <summary>
        /// Returns a statistic about the total charge per driver for a given month
        /// </summary>
        /// <param name="year">Year filter</param>
        /// <param name="month">Month filter</param>
        /// <returns>
        /// List of <see cref="DriverStatistics"/> objects containing the total charge for each driver in the given month.
        /// </returns>
        public async Task<List<DriverStatistics>> GetDriverStatisticsAsync(int year, int month)
        {
            return await Rides.Where(r => r.Start.Year == year && r.Start.Month == month && r.Charge.HasValue)
                .GroupBy(r => r.Driver.Name)
                .Select(r => new DriverStatistics
                {
                    DriverName = r.Key,
                    TotalCharge = r.Sum(x => x.Charge.Value)
                })
                .ToListAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
