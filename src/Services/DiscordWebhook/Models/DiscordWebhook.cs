using BizHawk.Emulation.Common;
using Newtonsoft.Json;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Pokemons;
using System.Collections.Generic;

namespace Pokebot.Services.DiscordWebhook.Models
{
    public class DiscordWebhook
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("tts")]
        public bool Tts { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("embeds")]
        public List<DiscordWebhookEmbed> Embeds { get; set; }

        public DiscordWebhook(string username, string content)
        {
            Content = content;
            Tts = false;
            Username = username;
            Embeds = new List<DiscordWebhookEmbed>();
        }

        public DiscordWebhook(string content, Pokemon pokemon, EncounterStats stats, GameVersion gameVersion, IGameInfo gameInfo) : this(Messages.AppName, content)
        {
            var embed = new DiscordWebhookEmbed($"{pokemon.RealName} ({pokemon.MetLevel})");
            embed.Thumbnail = new DiscordWebhookImage(
                pokemon.IsShiny
                ? $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/{pokemon.DexId}.png"
                : $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{pokemon.DexId}.png"
            );

            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Ability, pokemon.Ability ?? "N/A", false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Nature, pokemon.Nature?.Name ?? "N/A", false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Gender, pokemon.GetGenderMessage(), false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Item, pokemon.HeldItem?.Name ?? Messages.Item_Nothing, false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Shiny, pokemon.IsShiny ? Messages.Discord_ShinyYes : Messages.Discord_ShinyNo, false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVHp, pokemon.IVs.HP.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVSpeed, pokemon.IVs.Speed.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVAttack, pokemon.IVs.Attack.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVDefense, pokemon.IVs.Defense.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVSpAttack, pokemon.IVs.SpAttack.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_IVSpDefense, pokemon.IVs.SpDefense.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Trainer, pokemon.OriginalTrainer?.Name ?? "N/A", true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Game, gameInfo.Name, false));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_Encountered, stats.Encountered.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_ShinyEncountered, stats.ShinyEncountered.ToString(), true));
            embed.Fields.Add(new DiscordWebhookField(Messages.Discord_EncounteredRatio, stats.Ratio, true));

            Embeds.Add(embed);
        }
    }
}
