using Pets.Models.Pets;

namespace Pets.Models;

public class User
{
    private Pet[] ownedPets;
    private PetFood[] ownedFood;

    public int Id { get; set; }
    public double CashBalance { get; set; }
    public string UserName { get; set; }
    public Pet[] OwnedPets { get { return ownedPets; } set { ownedPets = value; } }
    public PetFood[] OwnedFood { get { return ownedFood; } set { ownedFood = value; } }

    public User(string name) 
    {
        UserName = name;
        CashBalance = 500;
        OwnedPets = new Pet[0];
        OwnedFood = new PetFood[0];
    }
}
