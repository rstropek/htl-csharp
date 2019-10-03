using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSpellChecker.Services
{
    public class WordComparer : IWordComparer
    {
        private readonly ILogger<WordComparer> logger;

        public WordComparer(ILogger<WordComparer> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> CompareWords(IEnumerable<string> sentenceWords, IEnumerable<string> dictionaryWords)
        {
            // For each word, check if it is contained in our dictionary.
            var results = new List<string>();
            foreach (var word in sentenceWords)
            {
                if (!dictionaryWords.Contains(word))
                {
                    logger.LogWarning("Found unknown word {word}", word);

                    // Return the word because it is not known
                    results.Add(word);
                }
            }

            return results;
        }
    }
}
