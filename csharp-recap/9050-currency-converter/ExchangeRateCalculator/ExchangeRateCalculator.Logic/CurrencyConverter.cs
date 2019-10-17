using System.Collections.Generic;

namespace ExchangeRateCalculator.Logic
{
    public class CurrencyConverter
    {
        private readonly Dictionary<string, decimal> rates;

        public CurrencyConverter(Dictionary<string, decimal> rates)
        {
            this.rates = rates;
        }

        public decimal Convert(decimal value, string from, string to)
        {
            if (from == to)
            {
                return value;
            }

            if (from != "EUR" && to != "EUR")
            {
                value = Convert(value, from, "EUR");
                from = "EUR";
            }

            if (from == "EUR")
            {
                return rates[to] * value;
            }

            // to == "EUR"
            return value / rates[from];
        }
    }
}
