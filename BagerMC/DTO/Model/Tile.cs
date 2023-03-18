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

        public Tile(int row, int column, TileContent tileContent)
        {
            Row = row;
            Column = column;
            TileContent = tileContent;
        }

        public Tile DeepCopy()
        {
            Tile deepcopyTile = new Tile(this.Row, this.Column, this.TileContent.DeepCopy());
            return deepcopyTile;
        }
    }
}
