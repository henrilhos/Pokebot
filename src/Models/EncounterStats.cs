using Pokebot.Models.Pokemons;

namespace Pokebot.Models
{
    public record EncounterStats(int Encountered, int ShinyEncountered, string Ratio, Pokemon Pokemon);
}
