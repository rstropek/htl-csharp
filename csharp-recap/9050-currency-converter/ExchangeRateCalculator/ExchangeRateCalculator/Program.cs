using ExchangeRateCalculator.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculator
{
    class Program
    {

        static async Task Main(string[] args)
        {
            // Parse exchange rates
            var xchangeParser = new ExchangeRateParser();
            var rates = xchangeParser.CsvToExchangeRates(
                await File.ReadAllTextAsync("ExchangeRates.csv"));

            // Parse products
            var productParser = new ProductParser();
            var products = productParser.CsvToProducts(
                await File.ReadAllTextAsync("Prices.csv"));

            // Look for product
            var product = products.FirstOrDefault(p => p.Description == args[0]);
            if (product == null)
            {
                Console.WriteLine("Sorry, product not found");
                return;
            }

            var converter = new CurrencyConverter(rates);
            Console.WriteLine($"{converter.Convert(product.Price, product.Currency, args[1]),0:0.00}");
        }
    }
}
