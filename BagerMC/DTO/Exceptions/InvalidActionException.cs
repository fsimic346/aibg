using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagerMC.DTO.Exceptions
{
    internal class InvalidActionException : Exception
    {
        public InvalidActionException()
        {

        }
        public InvalidActionException(string message) : base(message)
        {

        }
    }
}
