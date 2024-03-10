namespace PetsAPI.Requests.UserRequests;

public class CommandRequest
{
    public Guid Token { get; set; }
    public int PetId { get; set; }
    public int Command {  get; set; }
}
