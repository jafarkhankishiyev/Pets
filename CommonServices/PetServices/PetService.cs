using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.PetServices
{
    public abstract class PetService(Pet pet)
    {
        public Pet PetToServe { get; } = pet;

        public void MoodUp()
        {
            if (PetToServe.Mood != MoodType.Happy)
            {
                var m = (int)PetToServe.Mood;
                m++;
                PetToServe.Mood = (MoodType)m;
            }
        }

        public void HungerUp()
        {
            if (PetToServe.Hunger != HungerType.VeryHungry)
            {
                var h = (int)PetToServe.Hunger;
                h--;
                PetToServe.Hunger = (HungerType)h;
            }
        }

        public void MoodDown()
        {
            if (PetToServe.Mood != MoodType.Depressed)
            {
                var m = (int)PetToServe.Mood;
                m--;
                PetToServe.Mood = (MoodType)m;
            }
        }

        public void HungerDown()
        {
            if (PetToServe.Hunger != HungerType.Full)
            {
                var h = (int)PetToServe.Hunger;
                h++;
                PetToServe.Hunger = (HungerType)h;
            }
        }

        public void AffectByPlayOrCommand()
        {
            MoodUp();
            HungerUp();
        }

        public abstract string[] GetCommands();

        public abstract string[] GetCommandExecution();

        public abstract bool ValidateFood(FoodType f);
        public static PetService GetAccordingPetService(Pet pet)
        {
            PetService petService = pet switch
            {
                Bear => new BearService(pet),
                Cat => new CatService(pet),
                Dog => new DogService(pet),
                Parrot => new ParrotService(pet),
                _ => throw new Exception(),
            };
            return petService;
        }

        public void GetFed(User UserToServe, PetFood food)
        {
            PetService petService = PetService.GetAccordingPetService(PetToServe);
            if (petService.ValidateFood(food.FoodType))
            {
                petService.HungerDown();
                petService.MoodDown();
                UserToServe.OwnedFood[Array.IndexOf(UserToServe.OwnedFood, food)] = null;
                for (int i = 0; i < UserToServe.OwnedFood.Length; i++)
                {
                    if (UserToServe.OwnedFood[i] == null && UserToServe.OwnedFood.Length > 1)
                    {
                        if (i < UserToServe.OwnedFood.Length - 1)
                        {
                            UserToServe.OwnedFood[i] = UserToServe.OwnedFood[i + 1];
                            UserToServe.OwnedFood[i + 1] = null;
                        }
                    }
                }
                var arrToResize = UserToServe.OwnedFood;
                Array.Resize(ref arrToResize, UserToServe.OwnedFood.Length - 1);
                UserToServe.OwnedFood = arrToResize;
            }
        }

        public void GetPet()
        {
            MoodUp();
        }
    }
}
