
namespace CommonServices.Exceptions
{
    public class TakenUsernameException : Exception
    {
        public new string Message = "This username is taken!";
    }
}
