using BagerMC.DTO.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Model
{
    internal enum GameStateAction
    {
        Move, SkipATurn, FeedBeeWithNectar, ConvertNectarToHoney
    }
    internal class GameState
    {
        public BaseAction Action { get; set; }
        public Game State { get; set; }
    }
}
