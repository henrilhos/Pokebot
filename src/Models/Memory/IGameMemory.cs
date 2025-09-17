using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using System.Collections.Generic;

namespace Pokebot.Models.Memory
{
    public interface IGameMemory
    {
        VersionInfo VersionInfo { get; }
        HashData HashData { get; }
        GenerationInfo GenerationInfo { get; }

        int GetPartyCount();
        IReadOnlyList<Pokemon> GetParty();
        Pokemon GetOpponent();
        PlayerData GetPlayer();
        GameState GetGameState();
        ICollection<GTask> GetTasks();
        int GetActionSelectionCursor();
        void TrySetEscape();
        uint GetCurrentSeed();
        uint RandomizeCurrentSeed();
        int GetTID();
        int GetSID();
        IReadOnlyList<Symbol> GetSymbols();
        FishingState GetFishingResult();
        bool CanSetShiny();
        Pokemon SetShiny(Pokemon pokemon);
    }
}
