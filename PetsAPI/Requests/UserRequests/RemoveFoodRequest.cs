using Pets.Models.Enumerations;

namespace PetsAPI.Requests.UserRequests;

public record RemoveFoodRequest
{
    public Guid Token { get; set; }

    public FoodType FoodType { get; set; }
}
