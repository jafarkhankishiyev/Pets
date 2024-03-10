namespace PetsAPI.Requests.PetRequests;

public class GetPetRequest
{
    public Guid Token { get; set; }
    public int PetId { get; set; }
}
