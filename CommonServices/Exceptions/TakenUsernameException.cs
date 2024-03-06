using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Exceptions
{
    public class TakenUsernameException : Exception
    {
        public string Message = "This username is taken!";
    }
}
