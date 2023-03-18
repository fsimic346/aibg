using BagerMC.DTO.Model;

namespace BagerMC.Simulation
{
    public class Simulation
    {
        private GameAPI GameAPI { get; set; }
        public Simulation(GameAPI gameAPI)
        {
            GameAPI = gameAPI;
        }
        public bool IsGameFinished()
        {
            // 1. Uslov za kraj (dostignut potez 500)
            if (GameAPI.Game.NumOfMove >= 500)
            {
                GameAPI.Game.Finished = true;
                return true;
            }
            // 2. Uslov za kraj (nema cveca na mapi)
            if (IsBoardEmpty() == true)
            {
                GameAPI.Game.Finished = true;
                return true;
            }
            // 3. Uslov za kraj (SkipATurn > 150)
            if (GameAPI.Game.Player1.NumOfSkipATurnUsed == 150 || GameAPI.Game.Player2.NumOfSkipATurnUsed == 150)
            {
                GameAPI.Game.Finished = true;
                return true;
            }
            // 5. Uslov za kraj (Igrac stao na POND polje)

            // 6. Uslov za kraj (Igrac nema vise energije)
            if (GameAPI.Game.Player1.Energy == 0 || GameAPI.Game.Player2.Energy == 0)
            {
                GameAPI.Game.Finished = true;
                return true;
            }

            return false;
        }

        public bool IsBoardEmpty()
        {
            foreach (var tile in GameAPI.Game.Map.Tiles)
            {
                if (tile.TileContent.ItemType == ItemType.CHERRY_BLOSSOM || tile.TileContent.ItemType == ItemType.ROSE || tile.TileContent.ItemType == ItemType.LILAC || tile.TileContent.ItemType == ItemType.SUNFLOWER)
                    return false;
            }
            return true;
        }
    }
}
