using BagerMC.DTO.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Action
{
    public class SkipATurn : BaseAction
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        public async void Execute(GameAPI gameAPI)
        {
            var response = gameAPI.HttpClient.PostAsJsonAsync(gameAPI.BaseUrl + "skipATurn", this).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                gameAPI.Game = JsonConvert.DeserializeObject<Game>(jsonString);
            }
        }
    }
}
