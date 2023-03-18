using Newtonsoft.Json;

namespace BagerMC.DTO.Action
{
    public class Training
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("playerSpot")]
        public int PlayerSpot { get; set; }
    }
}
