using Newtonsoft.Json;

namespace BagerMC.DTO.Model
{
    public class Tile
    {
        [JsonProperty("row")]
        public int Row { get; set; }
        [JsonProperty("column")]
        public int Column { get; set; }
        [JsonProperty("tileContent")]
        public TileContent TileContent { get; set; }
    }
}
