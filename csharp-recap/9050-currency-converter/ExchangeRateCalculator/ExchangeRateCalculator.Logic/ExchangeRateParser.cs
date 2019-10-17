using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExchangeRateCalculator.Logic
{
    public class ExchangeRateParser : CsvParser
    {
        public Dictionary<string, decimal> CsvToExchangeRates(string text)
        {
            var csv = ReadCsv(text);
            var kvp = csv.Select(l => new KeyValuePair<string, decimal>(l[0], decimal.Parse(l[1], CultureInfo.InvariantCulture)));
            return new Dictionary<string, decimal>(kvp);
        }
    }
}
