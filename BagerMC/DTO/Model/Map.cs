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

        public Map(Tile[,] tiles, int width, int height)
        {
            Tiles = tiles;
            Width = width;
            Height = height;
        }

        public Map DeepCopy()
        {
            Tile[,] deepcopyTiles = new Tile[Width, Height];
            foreach (var item in Tiles)
            {
                deepcopyTiles[item.Row, item.Column] = item.DeepCopy();
            }
            Map deepcopyMap = new Map(deepcopyTiles, this.Width, this.Height);
            return deepcopyMap;
        }
    }
}
