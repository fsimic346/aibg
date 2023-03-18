using BagerMC.DTO.Model;

namespace BagerMC.Simulation
{
    public class Simulation
    {
        public bool IsGameFinished()
        {
            // 1. Uslov za kraj (dostignut potez 500)
            if (GlobalState.Game.NumOfMove >= 500)
            {
                GlobalState.Game.Finished = true;
                return true;
            }
            // 2. Uslov za kraj (nema cveca na mapi)
            if (IsBoardEmpty() == true)
            {
                GlobalState.Game.Finished = true;
                return true;
            }
            // 3. Uslov za kraj (SkipATurn > 150)
            if (GlobalState.Game.Player1.NumOfSkipATurnUsed == 150 || GlobalState.Game.Player2.NumOfSkipATurnUsed == 150)
            {
                GlobalState.Game.Finished = true;
                return true;
            }
            // 5. Uslov za kraj (Igrac stao na POND polje)

            // 6. Uslov za kraj (Igrac nema vise energije)
            if (GlobalState.Game.Player1.Energy == 0 || GlobalState.Game.Player2.Energy == 0)
            {
                GlobalState.Game.Finished = true;
                return true;
            }

            return false;
        }

        public bool IsBoardEmpty()
        {
            foreach (var tile in GlobalState.Game.Map.Tiles)
            {
                if (tile.TileContent.ItemType == ItemType.CHERRY_BLOSSOM || tile.TileContent.ItemType == ItemType.ROSE || tile.TileContent.ItemType == ItemType.LILAC || tile.TileContent.ItemType == ItemType.SUNFLOWER)
                    return false;
            }
            return true;
        }
    }
}
