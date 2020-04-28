using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TaxiManager.Shared;
using TaxiManager.UI;

/*
 * DO NOT CHANGE CODE IN THIS FILE DURING THE EXERCISE!
 */

namespace TaxiManager.Tests
{
    [TestClass]
    public class ViewModel : WebApiTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext context) => Setup();

        [ClassCleanup]
        public static void Cleanup() => Clean();

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Description("Verifies that the view model class implements INotifyPropertyChanged correctly")]
        public void ImplementINotifyPropertyChanged()
        {
            var vm = new MainWindowViewModel(null);
            Assert.IsInstanceOfType(vm, typeof(INotifyPropertyChanged));

            var notifiedPropertyNames = new List<string>();
            ((INotifyPropertyChanged)vm).PropertyChanged += (_, ea) => notifiedPropertyNames.Add(ea.PropertyName);
            vm.Charge = Decimal.MaxValue;
            vm.SelectedDriver = new Driver();
            vm.SelectedTaxi = new Taxi();

            CollectionAssert.AreEquivalent(new[] { "Charge", "SelectedDriver", "SelectedTaxi" }, notifiedPropertyNames);
        }

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Description("Verifies that the collections in the view model implement INotifyCollectionChanged correctly")]
        public void ImplementINotifyCollectionChanged()
        {
            var vm = new MainWindowViewModel(null);
            VerifyNotifyCollectionChanged(vm.Taxis, () => vm.Taxis.Add(null));
            VerifyNotifyCollectionChanged(vm.Drivers, () => vm.Drivers.Add(null));
            VerifyNotifyCollectionChanged(vm.CompletedRides, () => vm.CompletedRides.Add(null));
            VerifyNotifyCollectionChanged(vm.OngoingRides, () => vm.OngoingRides.Add(null));
        }

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Description("Verifies that the initialization method works correctly (2 Points)")]
        public async Task Initialize()
        {
            await DbContext.ClearAsync();
            await DbContext.AddDummyRideAsync();
            var (_, _, rideID) = await DbContext.AddDummyRideAsync();
            await DbContext.EndRideAsync(rideID, 99m);

            var vm = new MainWindowViewModel(Client);
            await vm.InitAsync();

            Assert.AreEqual(2, vm.Taxis.Count);
            Assert.AreEqual(2, vm.Drivers.Count);
            Assert.AreEqual(1, vm.OngoingRides.Count);
            Assert.AreEqual(1, vm.CompletedRides.Count);
        }

        [TestMethod]
        [Microsoft.VisualStudio.TestTools.UnitTesting.Description("Verifies that the view model ends rides correctly")]
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
            Assert.AreEqual(0, ongoingRides);
            Assert.IsTrue(hasNotified);
        }

        private void VerifyNotifyCollectionChanged(object target, Action changeAction)
        {
            var notifyingTarget = target as INotifyCollectionChanged;
            Assert.IsNotNull(notifyingTarget);

            var hasNotified = false;
            notifyingTarget.CollectionChanged += (_, __) => hasNotified = true;
            changeAction();
            Assert.IsTrue(hasNotified);
        }
    }
}
