using Pets.Models.Enumerations;

namespace PetsAPI.Models.PetRequests
{
    public class GetFedRequest
    {
        public Guid Token { get; set; }
        public int PetId { get; set; }
        public FoodType FoodType { get; set; }
    }
}
