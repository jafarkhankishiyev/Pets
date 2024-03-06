using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServices.DBServices.PetDB;
using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.PetServices
{
    public abstract class PetService(Pet pet, IPetDBService petDB)
    {
        public Pet PetToServe { get; } = pet;

        private IPetDBService PetDB { get; } = petDB;

        public async Task MoodUpAsync()
        {
            if (PetToServe.Mood != MoodType.Happy)
            {
                var m = (int)PetToServe.Mood;
                m++;
                PetToServe.Mood = (MoodType)m;

                await PetDB.SetMood(PetToServe.Mood);
            }
        }

        public async Task HungerUpAsync()
        {
            if (PetToServe.Hunger != HungerType.VeryHungry)
            {
                var h = (int)PetToServe.Hunger;
                h--;
                PetToServe.Hunger = (HungerType)h;

                await PetDB.SetHunger(PetToServe.Hunger);
            }
        }

        public async Task MoodDown()
        {
            if (PetToServe.Mood != MoodType.Depressed)
            {
                var m = (int)PetToServe.Mood;
                m--;
                PetToServe.Mood = (MoodType)m;

                await PetDB.SetMood(PetToServe.Mood);
            }
        }

        public async Task HungerDownAsync()
        {
            if (PetToServe.Hunger != HungerType.Full)
            {
                var h = (int)PetToServe.Hunger;
                h++;
                PetToServe.Hunger = (HungerType)h;

                await PetDB.SetHunger(PetToServe.Hunger);
            }
        }

        public async Task AffectByPlayOrCommandAsync()
        {
            await MoodUpAsync();
            await HungerUpAsync();
        }

        public abstract string[] GetCommands();

        public abstract string[] GetCommandExecution();

        public abstract bool ValidateFood(FoodType f);
        public static PetService GetAccordingPetService(Pet pet, IPetDBService petDB)
        {
            PetService petService = pet switch
            {
                Bear => new BearService(pet, petDB),
                Cat => new CatService(pet, petDB),
                Dog => new DogService(pet, petDB),
                Parrot => new ParrotService(pet, petDB),
                _ => throw new Exception(),
            };
            return petService;
        }

        public async Task GetFedAsync(User UserToServe, PetFood food)
        {
            PetService petService = PetService.GetAccordingPetService(PetToServe, PetDB);
            if (petService.ValidateFood(food.FoodType))
            {
                await petService.HungerDownAsync();
                await petService.MoodDown();
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

        public async Task GetPetAsync()
        {
            await MoodUpAsync();
        }
    }
}
