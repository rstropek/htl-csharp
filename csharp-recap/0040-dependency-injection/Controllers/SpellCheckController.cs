using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSpellChecker.Services;

namespace SimpleSpellChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpellCheckController : ControllerBase
    {
        private readonly IDictionaryReader reader;
        private readonly IWordComparer comparer;

        public SpellCheckController(IDictionaryReader reader, IWordComparer comparer)
        {
            this.reader = reader;
            this.comparer = comparer;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> SpellCheckGet([FromQuery] string sentence) => await SpellCheck(sentence);

        [HttpPost]
        public async Task<IEnumerable<string>> SpellCheck([FromBody] string sentence)
        {
            string dictionaryContent = await reader.ReadDictionaryAsync();

            // Split dictionary and sentence into words
            var dictionaryWords = dictionaryContent
                .Replace("\r", string.Empty)
                .Split('\n');
            var sentenceWords = sentence.Split(' ');

            var results = comparer.CompareWords(sentenceWords, dictionaryWords);

            return results;
        }
    }
}
