using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ExchangeRateCalculator.Logic
{
    public class ProductParser : CsvParser
    {
        public IEnumerable<Product> CsvToProducts(string text)
        {
            var csv = ReadCsv(text);
            return csv.Select(l => new Product
            {
                Description = l[0],
                Currency = l[1],
                Price = decimal.Parse(l[2], CultureInfo.InvariantCulture)
            });
        }
    }
}
