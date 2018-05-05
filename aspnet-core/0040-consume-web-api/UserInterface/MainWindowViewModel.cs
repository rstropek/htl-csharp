using Prism.Commands;
using Prism.Mvvm;
using Shared;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserInterface
{
    public class MainWindowViewModel : BindableBase
    {
        private HttpClient HttpClient { get; }

        public MainWindowViewModel()
        {
            // Create HttpClient with base address
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:60215")
            };

            // Add event handler for refresh button
            Refresh = new DelegateCommand(async () => await RefreshAsync());

            // Create property for DataGrid binding
            Customers = new ObservableCollection<Customer>();
        }

        public DelegateCommand Refresh { get; }

        public ObservableCollection<Customer> Customers { get; }

        private async Task RefreshAsync()
        {
            // Execute HTTP GET
            var response = await HttpClient.GetAsync("/api/customers");
            response.EnsureSuccessStatusCode();

            // Transfer result into data-bound collection
            Customers.Clear();
            foreach(var c in await response.Content.ReadAsAsync<Customer[]>())
            {
                Customers.Add(c);
            }
        }
    }
}
