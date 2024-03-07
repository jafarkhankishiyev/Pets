using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace PetsAPI.Models.UserRequests
{
    public class AddPetRequest
    {
        public Guid Token { get; set; }
        public string PetName { get; set; }
        public FurType FurColor { get; set; }
        public int PetType { get; set; }
    }
}
