
namespace CommonServices.Exceptions;

public class WrongFoodException : Exception
{
    public new string Message = "This food is not appropriate for this pet.";
}
