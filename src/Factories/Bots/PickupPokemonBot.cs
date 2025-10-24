using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using System.Linq;

namespace Pokebot.Factories.Bots
{
    public class PickupPokemonBot : StaticBot
    {
        public PickupPokemonBot(ApiContainer apiContainer, GameVersion gameVersion) : base(apiContainer, gameVersion)
        {

        }

        private int _startPartySize = 0;

        public override void Start()
        {
            var partySize = GameVersion.Memory.GetPartyCount();
            if (partySize >= 6)
            {
                throw new BotException(Messages.BotPickupPokemon_PartyFull);
            }

            _startPartySize = partySize;

            base.Start();
        }

        public override void Execute(PlayerData playerData, GameState state)
        {
            if (GameVersion.Memory.GetPartyCount() > _startPartySize)
            {
                Pokemon pokemon = GameVersion.Memory.GetParty().Last();
                Check(pokemon);
            }
            else
            {
                APIContainer.Joypad.Set("A", true);
            }
        }

        protected override string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_pickup_pokemon";
        }
    }
}
