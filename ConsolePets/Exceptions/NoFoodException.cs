using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePets.Exceptions
{
    internal class NoFoodException : Exception
    {
        public NoFoodException() 
        {
            Console.WriteLine("You have no food! You may get some in the shop.");
        }
    }
}
