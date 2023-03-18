using Newtonsoft.Json;

namespace BagerMC.DTO.Model
{

    public enum ExecutedAction
    {
        MOVE,
        MOVE_IMPOSSIBLE,
        MOVE_ON_POND,
        CONVERT_NECTAR_TO_HONEY,
        SKIP_A_TURN,
        FEED_BEE_WITH_NECTAR,
    }
    public class Player
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("energy")]
        public int Energy { get; set; }
        [JsonProperty("nectar")]
        public int Nectar { get; set; }
        [JsonProperty("honey")]
        public int Honey { get; set; }
        [JsonProperty("turnsFrozen")]
        public int TurnsFrozen { get; set; }
        [JsonProperty("numOfSkipATurnUsed")]
        public int NumOfSkipATurnUsed { get; set; }
        [JsonProperty("executedAction")]
        public ExecutedAction? ExecutedAction { get; set; }
        [JsonProperty("distanceMoved")]
        public int DistanceMoved { get; set; }
        [JsonProperty("teamName")]
        public string TeamName { get; set; }
        [JsonProperty("skin")]
        public int Skin { get; set; }
        [JsonProperty("hiveX")]
        public int HiveX { get; set; }
        [JsonProperty("hiveY")]
        public int HiveY { get; set; }
        [JsonProperty("frozen")]
        public bool Frozen { get; set; }
    }
}