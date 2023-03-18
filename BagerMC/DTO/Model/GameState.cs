using BagerMC.DTO.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Model
{
    public class GameState
    {
        public BaseAction Action { get; set; }
        public Game State { get; set; }
        public int Evaluation { get; set; }
    }
}
