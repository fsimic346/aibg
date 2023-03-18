using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace BagerMC.DTO.Model
{
    public class Map
    {
        [JsonProperty("tiles")]
        public Tile[,] Tiles { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
