using Pets.Models.Enumerations;

namespace PetsAPI.Models.UserRequests
{
    public class RemoveFoodRequest
    {
        public Guid Token { get; set; }

        public FoodType FoodType { get; set; }
    }
}
