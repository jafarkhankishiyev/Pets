
namespace CommonServices.Exceptions;

public class WrongNameException : Exception
{
    public new string Message = "A name should not be empty.";
}
