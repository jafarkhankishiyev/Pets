using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Exceptions
{
    internal class WrongFoodException : Exception
    {
        public WrongFoodException() 
        {
            Console.WriteLine("This food is not appropriate for this pet.");
        }
    }
}
