using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Panels;
using Pokebot.Utils;
using System;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    internal class HeadbuttBot : IBot
    {
        public bool Enabled { get; private set; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.PokemonFoundEventHandler? PokemonFound;
        public event IBot.StateChangedEventHandler? StateChanged;

        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public SpinControl Control { get; }

        private Pokemon? _lastEncountered;
        private int _nbTry;

        public HeadbuttBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            Control = new SpinControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(GameVersion.GenerationInfo);
        }

        public void Start()
        {
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            var state = GameVersion.Memory.GetGameState();
            if (state != GameState.Overworld)
            {
                throw new BotException(Messages.SpinBot_StartOnlyMap);
            }
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            if (state == GameState.Battle || state == GameState.BagMenu)
            {
                try
                {
                    Pokemon pokemon = GameVersion.Memory.GetOpponent();

                    if (pokemon != null && _lastEncountered?.Checksum != pokemon.Checksum)
                    {
                        _lastEncountered = pokemon;
                        PokemonEncountered?.Invoke(pokemon);
                    }

                    if (pokemon != null && Control.FilterPanel.Comparator.Compare(pokemon))
                    {
                        Log.Warn(Messages.Pokemon_FoundCatch);
                        PokemonFound?.Invoke(pokemon);
                        Stop();
                    }
                    else
                    {
                        GameVersion.Runner.Escape();
                    }
                }
                catch (Exception ex)
                {
                    // Sometimes when entering battle the pokemon data is not ready yet
                    if (_nbTry > 10)
                    {
                        throw ex;
                    }
                    _nbTry++;
                }
            }
            else if (state == GameState.Overworld)
            {
                _nbTry = 0;
                _lastEncountered = null;
                GameVersion.Runner.Headbutt();
            }
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        public bool UseDelay()
        {
            return false;
        }

        public void UpdateUI(GameState state)
        {
        }
    }
}
