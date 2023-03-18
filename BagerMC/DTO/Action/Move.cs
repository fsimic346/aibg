using Newtonsoft.Json;

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
    public class Move
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
        [JsonProperty("distance")]
        public int Distance { get; set; }
    }
}
