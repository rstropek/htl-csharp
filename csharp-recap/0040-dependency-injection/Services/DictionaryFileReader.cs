using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SimpleSpellChecker.Controllers;
using System.IO;
using System.Threading.Tasks;

namespace SimpleSpellChecker.Services
{
    public class DictionaryFileReader : IDictionaryReader
    {
        private readonly IConfiguration config;
        private readonly ILogger<SpellCheckController> logger;

        public DictionaryFileReader(IConfiguration config, ILogger<SpellCheckController> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public async Task<string> ReadDictionaryAsync()
        {
            string dictionaryContent;
            try
            {
                // Read dictionary from file
                var dictFileName = config["dictionaryFileName"];
                dictionaryContent = await System.IO.File.ReadAllTextAsync(dictFileName);
            }
            catch (FileNotFoundException ex)
            {
                logger.LogCritical(ex, "Dictionary not found");
                throw;
            }

            return dictionaryContent;
        }
    }
}
