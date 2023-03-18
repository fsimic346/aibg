using BagerMC.DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Action
{
    public interface BaseAction
    {        
        public void Execute(GameAPI gameAPI);
    }
}
