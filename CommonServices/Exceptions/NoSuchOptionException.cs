using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Exceptions
{
    internal class NoSuchOptionException : Exception
    {
        public NoSuchOptionException() 
        {
            Console.WriteLine("No such option!");
        }
    }
}
