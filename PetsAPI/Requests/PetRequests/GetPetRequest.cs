namespace PetsAPI.Requests.PetRequests;

public record GetPetRequest
{
    public Guid Token { get; set; }
    public int PetId { get; set; }
}
