using System.Collections.Generic;

namespace SimpleSpellChecker.Services
{
    public interface IWordComparer
    {
        IEnumerable<string> CompareWords(IEnumerable<string> sentenceWords, IEnumerable<string> dictionaryWords);
    }
}
