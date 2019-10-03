using System.Threading.Tasks;

namespace SimpleSpellChecker.Services
{
    public interface IDictionaryReader
    {
        Task<string> ReadDictionaryAsync();
    }
}
