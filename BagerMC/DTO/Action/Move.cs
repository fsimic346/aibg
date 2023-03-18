using BagerMC.DTO.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BagerMC.DTO.Action
{
    public enum Direction
    {
        q,
        w,
        e,
        d,
        s,
        a
    }
    public class Move : BaseAction
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
        [JsonProperty("distance")]
        public int Distance { get; set; }

        public async void Execute(GameAPI gameAPI)
        {
            var response = gameAPI.HttpClient.PostAsJsonAsync(gameAPI.BaseUrl + "move", this).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                gameAPI.Game = JsonConvert.DeserializeObject<Game>(jsonString);
            }
        }
    }
}
