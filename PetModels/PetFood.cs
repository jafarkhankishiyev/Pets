using Pets.Models.Enumerations;

namespace Pets.Models;

public class PetFood
{
    public double Price { get; init; }
    public string Name { get; init; }
    public FoodType FoodType { get; init; }

    public PetFood()
    {
        Price = 0;
        Name = string.Empty;
    }

    public PetFood(FoodType food)
    {
        FoodType = food;
        switch (food)
        {
            case FoodType.ParrotFood:
                Price = 3.99;
                Name = "Parrot Food";
                break;
            case FoodType.BearFood:
                Price = 7.99;
                Name = "Bear Food";
                break;
            case FoodType.CatFood:
                Price = 4.99;
                Name = "Cat Food";
                break;
            case FoodType.DogFood:
                Price = 5.99;
                Name = "Dog Food";
                break;
        }
    }
}
