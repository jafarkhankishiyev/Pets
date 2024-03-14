namespace PetsAPI.Requests.UserRequests;

public record CommandRequest
{
    public Guid Token { get; set; }
    public int PetId { get; set; }
    public int Command {  get; set; }
}
