using Pets.Models.Enumerations;
using Pets.Models.Pets;
using Pets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommonServices.PetServices;
using Pets.Models.Enumerations.PetCommands;

namespace CommonServices
{
    public class UserService
    {
        public User UserToServe { get; set; }

        public UserService(User user)
        {
            UserToServe = user;
        }

        public void Work()
        {
            foreach (Pet p in UserToServe.OwnedPets)
            {
                PetService petService = PetService.GetAccordingPetService(p);
                petService.MoodDown();
            }
            UserToServe.CashBalance += 50;
        }

        public PetFood Command(PetService petService, int commandExecuted)
        {
            petService.AffectByPlayOrCommand();

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
                return petFood;
            }
            return null;
        }

        public void BuyFood(FoodType petFoodType)
        {
                PetFood petFood = new PetFood(petFoodType);
                var arrToResize = UserToServe.OwnedFood;
                Array.Resize(ref arrToResize, UserToServe.OwnedFood.Length + 1);
                UserToServe.OwnedFood = arrToResize;
                UserToServe.OwnedFood[^1] = petFood;
                UserToServe.CashBalance -= petFood.Price;
        }

        public bool AddPet(Pet pet)
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
                        return true;
                    }
                }
                else
                    return false;
        }
    }
}
