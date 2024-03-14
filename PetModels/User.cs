using Models;
using Pets.Models.Pets;

namespace Pets.Models;

public class User : BaseModel
{
    private Pet[] ownedPets;
    private PetFood[] ownedFood;

    public double CashBalance { get; set; }
    public string Username { get; set; }
    public Pet[] OwnedPets { get { return ownedPets; } set { ownedPets = value; } }
    public PetFood[] OwnedFood { get { return ownedFood; } set { ownedFood = value; } }

    public User(string name) 
    {
        Username = name;
        CashBalance = 0;
        OwnedPets = new Pet[0];
        OwnedFood = new PetFood[0];
    }
}
