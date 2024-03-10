using Pets.Models.Enumerations;
using Pets.Models.Pets;
using Pets.Models;
using System.Security.Cryptography;
using CommonServices.PetServices;
using Pets.Models.Enumerations.PetCommands;
using CommonServices.DBServices.UserDB;
using CommonServices.DBServices.PetDB;

namespace CommonServices;

public class UserService
{
    public User UserToServe { get; set; }

    public IUserDBService UserDB { get; }

    public UserService(IUserDBService userDB)
    {
        UserToServe = userDB.UserToServe;
        UserDB = userDB;
    }

    public async Task WorkAsync()
    {
        foreach (Pet p in UserToServe.OwnedPets)
        {
            var petService = PetService.GetAccordingPetService(new PostgresPetDBService(UserDB.UserDataSource.ConnectionString, p));
            await petService.MoodDownAsync();
        }

        UserToServe.CashBalance += 50;
        
        await UserDB.SetCash(UserToServe.CashBalance);
    }

    public async Task<PetFood> CommandAsync(PetService petService, int commandExecuted)
    {
        await petService.AffectByPlayOrCommandAsync();

        if (petService is BearService && (BearCommands)commandExecuted is BearCommands.Forest)
        {
            int randomInt = RandomNumberGenerator.GetInt32(4);
            randomInt++;
            PetFood petFood = new();
            petFood = randomInt switch
            {
                1 => new PetFood(FoodType.CatFood),
                2 => new PetFood(FoodType.DogFood),
                3 => new PetFood(FoodType.BearFood),
                4 => new PetFood(FoodType.ParrotFood),
                _ => new PetFood()
            };
            var arrToResize = UserToServe.OwnedFood;
            Array.Resize(ref arrToResize, 1);
            UserToServe.OwnedFood = arrToResize;
            UserToServe.OwnedFood[^1] = petFood;

            await UserDB.AddFood(petFood);
            return petFood;
        }
        return null;
    }

    public async Task BuyFoodAsync(FoodType petFoodType)
    {
        PetFood petFood = new PetFood(petFoodType);
        var arrToResize = UserToServe.OwnedFood;
        Array.Resize(ref arrToResize, UserToServe.OwnedFood.Length + 1);
        UserToServe.OwnedFood = arrToResize;
        UserToServe.OwnedFood[^1] = petFood;
        UserToServe.CashBalance -= petFood.Price;

        await UserDB.AddFood(petFood);
        await UserDB.SetCash(UserToServe.CashBalance);    
    }

    public async Task<bool> AddPetAsync(Pet pet)
    {
        if (pet != null)
        {
            if (UserToServe.OwnedPets.Length == 1 && UserToServe.OwnedPets[0].Name == "None")
            {
                UserToServe.OwnedPets[0] = pet;
                return true;
            }
            else
            {
                var arrToResize = UserToServe.OwnedPets;
                Array.Resize(ref arrToResize, UserToServe.OwnedPets.Length + 1);
                UserToServe.OwnedPets = arrToResize;
                UserToServe.OwnedPets[^1] = pet;

                await UserDB.AddPet(pet);

                return true;
            }
        }
        else
            return false;
    }
}
