using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.WebApi.Data;
using Xunit;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    public class ViewModel : IClassFixture<WebApiFactory>
    {
        public HttpClient Client { get;  }

        public TaxiDataContext DbContext { get; }

        public WebApiFactory Factory { get; }

        public ViewModel(WebApiFactory factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
            DbContext = Factory.Services.GetService<TaxiDataContext>();
        }

        [Fact(DisplayName = "Verifies that the view model class implements INotifyPropertyChanged correctly")]
        public void ImplementINotifyPropertyChanged()
        {
            var vm = new MainWindowViewModel(null);
            Assert.IsAssignableFrom<INotifyPropertyChanged>(vm);

            var notifiedPropertyNames = new List<string>();
            ((INotifyPropertyChanged)vm).PropertyChanged += (_, ea) => notifiedPropertyNames.Add(ea.PropertyName);
            vm.Charge = Decimal.MaxValue;
            vm.SelectedDriver = new Driver();
            vm.SelectedTaxi = new Taxi();

            Assert.Equal(new[] { "Charge", "SelectedDriver", "SelectedTaxi" }, notifiedPropertyNames);
        }

        [Fact(DisplayName = "Verifies that the collections in the view model implement INotifyCollectionChanged correctly")]
        public void ImplementINotifyCollectionChanged()
        {
            var vm = new MainWindowViewModel(null);
            VerifyNotifyCollectionChanged(vm.Taxis, () => vm.Taxis.Add(null));
            VerifyNotifyCollectionChanged(vm.Drivers, () => vm.Drivers.Add(null));
            VerifyNotifyCollectionChanged(vm.CompletedRides, () => vm.CompletedRides.Add(null));
            VerifyNotifyCollectionChanged(vm.OngoingRides, () => vm.OngoingRides.Add(null));
        }

        [Fact(DisplayName = "Verifies that the initialization method works correctly (2 Points)")]
        public async Task Initialize()
        {
            await DbContext.ClearAsync();
            await DbContext.AddDummyRideAsync();
            var (_, _, rideID) = await DbContext.AddDummyRideAsync();
            await DbContext.EndRideAsync(rideID, 99m);

            var vm = new MainWindowViewModel(Client);
            await vm.InitAsync();

            Assert.Equal(2, vm.Taxis.Count);
            Assert.Equal(2, vm.Drivers.Count);
            Assert.Single(vm.OngoingRides);
            Assert.Single(vm.CompletedRides);
        }

        [Fact(DisplayName = "Verifies that the view model ends rides correctly")]
        public async Task EndRide()
        {
            await DbContext.ClearAsync();
            await DbContext.AddDummyRideAsync();

            var hasNotified = false;
            var vm = new MainWindowViewModel(Client);
            ((INotifyPropertyChanged)vm).PropertyChanged += (_, ea) => hasNotified = true; ;

            await vm.InitAsync();
            vm.SelectedOngoingRide = vm.OngoingRides.First();
            vm.Charge = 99m;
            await vm.EndRideAsync();

            var ongoingRides = await DbContext.Rides.CountAsync(r => !r.Charge.HasValue || !r.End.HasValue);
            Assert.Equal(0, ongoingRides);
            Assert.True(hasNotified);
        }

        private void VerifyNotifyCollectionChanged(object target, Action changeAction)
        {
            var notifyingTarget = target as INotifyCollectionChanged;
            Assert.NotNull(notifyingTarget);

            var hasNotified = false;
            notifyingTarget.CollectionChanged += (_, __) => hasNotified = true;
            changeAction();
            Assert.True(hasNotified);
        }
    }
}
