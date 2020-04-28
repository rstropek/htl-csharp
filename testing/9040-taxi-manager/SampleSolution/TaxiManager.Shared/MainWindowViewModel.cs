using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManager.Shared
{
    public class MainWindowViewModel : BindableBase
    {
        private HttpClient HttpClient { get; }

        private readonly AsyncRetryPolicy RetryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(5);

        public MainWindowViewModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
            StartRideCommand = new DelegateCommand(async () => await StartRideAsync(), () => SelectedDriver != null && SelectedTaxi != null);
            EndRideCommand = new DelegateCommand(async () => await EndRideAsync(), () => SelectedOngoingRide != null && Charge != null);
            PropertyChanged += (_, __) =>
            {
                StartRideCommand.RaiseCanExecuteChanged();
                EndRideCommand.RaiseCanExecuteChanged();
            };
        }

        /// <summary>
        /// Initializes the view-model
        /// </summary>
        /// <remarks>
        /// Fills <see cref="Taxis"/> by reading all taxis from the Web API /api/taxis.
        /// Fills <see cref="Drivers"/> by reading all drivers from the Web API /api/drivers.
        /// Fills <see cref="OngoingRides"/> by reading all ongoing rides from the Web API /api/rides/ongoing.
        /// Fills <see cref="CompletedRides"/> by reading all completed rides from the Web API /api/rides/completed.
        /// </remarks>
        public async Task InitAsync()
        {
            Taxis = await GetFromServiceAsync<ObservableCollection<Taxi>>("/api/taxis");
            Drivers = await GetFromServiceAsync<ObservableCollection<Driver>>("/api/drivers");
            await RefreshRidesAsync();
        }

        private ObservableCollection<Taxi> taxis = new ObservableCollection<Taxi>();
        public ObservableCollection<Taxi> Taxis
        {
            get { return taxis; }
            set { SetProperty(ref taxis, value); }
        }

        private ObservableCollection<Driver> drivers = new ObservableCollection<Driver>();
        public ObservableCollection<Driver> Drivers
        {
            get { return drivers; }
            set { SetProperty(ref drivers, value); }
        }

        private ObservableCollection<TaxiRide> ongoingRides = new ObservableCollection<TaxiRide>();
        public ObservableCollection<TaxiRide> OngoingRides
        {
            get { return ongoingRides; }
            set { SetProperty(ref ongoingRides, value); }
        }

        private ObservableCollection<TaxiRide> completedRides = new ObservableCollection<TaxiRide>();
        public ObservableCollection<TaxiRide> CompletedRides
        {
            get { return completedRides; }
            set { SetProperty(ref completedRides, value); }
        }

        private Driver selectedDriver;
        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set { SetProperty(ref selectedDriver, value); }
        }

        private Taxi selectedTaxi;
        public Taxi SelectedTaxi
        {
            get { return selectedTaxi; }
            set { SetProperty(ref selectedTaxi, value); }
        }

        private decimal? charge;
        public decimal? Charge
        {
            get { return charge; }
            set { SetProperty(ref charge, value); }
        }

        private TaxiRide selectedOngoingRide;
        public TaxiRide SelectedOngoingRide
        {
            get { return selectedOngoingRide; }
            set { SetProperty(ref selectedOngoingRide, value); }
        }

        public DelegateCommand StartRideCommand { get; }

        public DelegateCommand EndRideCommand { get; }

        /// <summary>
        /// Start a new ongoing taxi ride.
        /// </summary>
        /// <remarks>
        /// Takes the currently selected taxi (<see cref="SelectedTaxi"/>) and the currently selected driver
        /// (<see cref="SelectedDriver"/>) and creates a new ongoing ride using HTTP POST to the web api /api/rides.
        /// After the new ride was created, the list of ongoing rides (<see cref="OngoingRides"/>) and
        /// completed rides (<see cref="CompletedRides"/>) have to be refreshed.
        /// </remarks>
        public async Task StartRideAsync()
        {
            var startRide = new StartRideDto
            {
                TaxiID = SelectedTaxi.ID,
                DriverID = SelectedDriver.ID
            };
            await PostToServiceAsync("/api/rides", startRide);
            await RefreshRidesAsync();
        }

        /// <summary>
        /// Ends an ongoing taxi ride.
        /// </summary>
        /// <remarks>
        /// Takes the currently selected ride (<see cref="SelectedOngoingRide"/>) and the currently entered charge
        /// (<see cref="Charge"/>) and ends the ongoing ride using HTTP POST to the web api /api/rides/{id}.
        /// After the new ride was ended, the list of ongoing rides (<see cref="OngoingRides"/>) and
        /// completed rides (<see cref="CompletedRides"/>) have to be refreshed.
        /// </remarks>
        public async Task EndRideAsync()
        {
            var endRide = new EndRideDto { Charge = Charge.Value };
            await PostToServiceAsync($"/api/rides/{SelectedOngoingRide.ID}", endRide);
            await RefreshRidesAsync();
        }

        private async Task RefreshRidesAsync()
        {
            var ongoing = GetFromServiceAsync<IEnumerable<TaxiRide>>("/api/rides/ongoing");
            var completed = GetFromServiceAsync<IEnumerable<TaxiRide>>("/api/rides/completed");
            await Task.WhenAll(ongoing, completed);

            CompletedRides.Clear();
            foreach (var r in completed.Result)
            {
                CompletedRides.Add(r);
            }

            OngoingRides.Clear();
            foreach (var r in ongoing.Result)
            {
                OngoingRides.Add(r);
            }
        }

        /// <summary>
        /// Helper method to GET data from a RESTful Web API
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="url">URL of the service</param>
        /// <returns>Async result returned from the Web API</returns>
        private async Task<T> GetFromServiceAsync<T>(string url)
        {
            var resultString = await RetryPolicy.ExecuteAndCaptureAsync(async () => await HttpClient.GetStringAsync(url));
            return JsonConvert.DeserializeObject<T>(resultString.Result);
        }

        /// <summary>
        /// Helper method to POST data to a RESTful Web API
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="payload"/> that has to be sent</typeparam>
        /// <param name="url">URL of the service</param>
        /// <param name="payload">Object to send to the Web API</param>
        /// <returns>Task representing the async operation</returns>
        private async Task PostToServiceAsync<T>(string url, T payload)
        {
            var body = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, body);
            response.EnsureSuccessStatusCode();
        }
    }
}
