using BagerMC.DTO.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BagerMC.DTO.Action
{
    public class ConvertNectarToHoney : BaseAction
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("amountOfHoneyToMake")]
        public int AmountOfHoneyToMake { get; set; }

        public async void Execute(GameAPI gameAPI)
        {
            var response = gameAPI.HttpClient.PostAsJsonAsync(gameAPI.BaseUrl + "convertNectarToHoney", this).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                gameAPI.Game = JsonConvert.DeserializeObject<Game>(jsonString);
            }
        }
    }
}
