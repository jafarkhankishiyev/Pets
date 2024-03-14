using Pets.Models.Enumerations;

namespace PetsAPI.Requests.UserRequests;

public record AddPetRequest
{
    public Guid Token { get; set; }
    public string PetName { get; set; }
    public FurType FurColor { get; set; }
    public int PetType { get; set; }
}
