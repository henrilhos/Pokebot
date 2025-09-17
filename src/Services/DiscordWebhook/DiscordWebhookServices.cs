using BizHawk.Emulation.Common;
using Newtonsoft.Json;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Pokemons;
using Pokebot.Utils;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Services.DiscordWebhook
{
    public class DiscordWebhookServices
    {
        public string Url { get; }
        public string UserID { get; }
        public DiscordWebhookServices(string url, string userID)
        {
            Url = url;
            UserID = userID;
        }

        public void SendPokemonWebhook(Pokemon pokemon, EncounterStats stats, GameVersion gameVersion, IGameInfo gameInfo)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Url))
                {
                    string content;
                    if (string.IsNullOrWhiteSpace(UserID))
                    {
                        content = Messages.Discord_Content;
                    }
                    else
                    {
                        var pingUserID = $"<@{UserID}>";
                        content = string.Format(Messages.Discord_ContentWithUser, pingUserID, Messages.Discord_Content);
                    }

                    var webhook = new Models.DiscordWebhook(content, pokemon, stats, gameVersion, gameInfo);
                    var json = JsonConvert.SerializeObject(webhook);

                    Task.Run(async () =>
                    {
                        try
                        {
                            using (var client = new HttpClient())
                            {
                                var content = new StringContent(json, Encoding.UTF8, "application/json");
                                await client.PostAsync(Url, content);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format(Messages.DiscordWebhook_Failed, ex.Message));
            }
        }
    }
}
