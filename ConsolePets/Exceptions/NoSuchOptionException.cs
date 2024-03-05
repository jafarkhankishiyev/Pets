using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePets.Exceptions
{
    internal class NoSuchOptionException : Exception
    {
        public NoSuchOptionException() 
        {
            Console.WriteLine("No such option!");
        }
    }
}
