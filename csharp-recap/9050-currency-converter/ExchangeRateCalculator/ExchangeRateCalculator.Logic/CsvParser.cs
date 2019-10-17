using System.Collections.Generic;
using System.Linq;

namespace ExchangeRateCalculator.Logic
{
    public class CsvParser
    {
        protected IEnumerable<string[]> ReadCsv(string fileContent) =>
            fileContent.Replace("\r", "").Split('\n')
                .Skip(1)
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Split(','));
    }
}
