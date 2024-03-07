using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Exceptions
{
    internal class WrongNameException : Exception
    {
        public WrongNameException() 
        {
            Console.WriteLine("A name should not be empty or contain numbers.");
        }
    }
}
