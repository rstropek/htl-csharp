using System;
using System.Configuration;
using System.Net.Http;
using System.Windows;

/*
 * NO NEED TO CHANGE SOMETHING IN THIS FILE DURING THE EXAM
 */

namespace TaxiManager.UI
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel = new MainWindowViewModel(new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceBaseUrl"])
            });
            Loaded += async (_, __) => await ViewModel.InitAsync();
        }
    }
}
