using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxiManager.Shared;

namespace TaxiManager.UI
{
    public class MainWindowViewModel
    {
        private HttpClient HttpClient { get; set; }

        private readonly AsyncRetryPolicy RetryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(5);

        public MainWindowViewModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
            StartRideCommand = new DelegateCommand(async () => await StartRideAsync(), () => SelectedDriver != null && SelectedTaxi != null);
            EndRideCommand = new DelegateCommand(async () => await EndRideAsync(), () => SelectedOngoingRide != null && Charge != null);

            // Remove the comment from the next few lines once you have implemented
            // the necessary INotifyPropertyChanged interface.
            //PropertyChanged += (_, __) =>
            //{
            //    StartRideCommand.RaiseCanExecuteChanged();
            //    EndRideCommand.RaiseCanExecuteChanged();
            //};
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
        public Task InitAsync()
        {
            throw new NotImplementedException();
        }

        public List<Taxi> Taxis { get; set; }

        public List<Driver> Drivers { get; set; }

        public List<TaxiRide> OngoingRides { get; set; }

        public List<TaxiRide> CompletedRides { get; set; }

        public Driver SelectedDriver { get; set; }

        public Taxi SelectedTaxi { get; set; }

        public decimal? Charge { get; set; }

        public TaxiRide SelectedOngoingRide { get; set; }

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
        public Task StartRideAsync()
        {
            throw new NotImplementedException();
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
        public Task EndRideAsync()
        {
            throw new NotImplementedException();
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
