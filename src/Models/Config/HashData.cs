using Newtonsoft.Json;

namespace Pokebot.Models.Config
{
    public class HashData
    {
        public string Hash { get; }
        public bool Tested { get; }
        public HashSymbols Symbols { get; }

        [JsonConstructor]
        public HashData(string hash, bool tested, HashSymbols symbols)
        {
            Hash = hash;
            Tested = tested;
            Symbols = symbols;
        }
    }
}
