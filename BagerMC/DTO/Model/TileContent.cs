using Newtonsoft.Json;

namespace BagerMC.DTO.Model
{
    public enum ItemType
    {
        CHERRY_BLOSSOM,
        ROSE,
        LILAC,
        SUNFLOWER,
        ENERGY,
        BOOSTER_NECTAR_30_PCT,
        BOOSTER_NECTAR_50_PCT,
        BOOSTER_NECTAR_100_PCT,
        HIVE,
        POND,
        EMPTY,
        MINUS_FIFTY_PCT_ENERGY,
        SUPER_HONEY,
        FREEZE,
        HOLE
    }
    public class TileContent
    {
        [JsonProperty("itemType")]
        public ItemType ItemType { get; set; }
        [JsonProperty("numOfItems")]
        public int NumOfItems { get; set; }

        public TileContent(ItemType itemType, int numOfItems)
        {
            ItemType = itemType;
            NumOfItems = numOfItems;
        }

        public TileContent DeepCopy()
        {
            TileContent deepcopyTileContent = new TileContent(this.ItemType, this.NumOfItems);
            return deepcopyTileContent;
        }
    }
}
