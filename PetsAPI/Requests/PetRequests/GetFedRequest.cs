using Pets.Models.Enumerations;

namespace PetsAPI.Requests.PetRequests
{
    public record GetFedRequest
    {
        public Guid Token { get; set; }
        public int PetId { get; set; }
        public FoodType FoodType { get; set; }
    }
}
