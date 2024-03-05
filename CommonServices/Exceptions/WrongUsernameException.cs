using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Exceptions;

public class WrongUsernameException : Exception
{
    public string _message = "Wrong Username!"; 
}
