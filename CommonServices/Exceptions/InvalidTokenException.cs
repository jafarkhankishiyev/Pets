
namespace CommonServices.Exceptions;

public class InvalidTokenException : Exception
{
    public new string Message = "Token is no longer valid";
}
