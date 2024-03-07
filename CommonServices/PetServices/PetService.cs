using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServices.DBServices.PetDB;
using CommonServices.Exceptions;
using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.PetServices
{
    public abstract class PetService(IPetDBService petDB)
    {
        public Pet PetToServe { get; } = petDB.PetToServe;

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

        public async Task MoodDownAsync()
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
        public static PetService GetAccordingPetService(IPetDBService petDB)
        {
            PetService petService = petDB.PetToServe switch
            {
                Bear => new BearService(petDB),
                Cat => new CatService(petDB),
                Dog => new DogService(petDB),
                Parrot => new ParrotService(petDB),
                _ => throw new Exception(),
            };
            return petService;
        }

        public async Task GetFedAsync(User UserToServe, PetFood food)
        {
            PetService petService = PetService.GetAccordingPetService(PetDB);

            if(!petService.ValidateFood(food.FoodType)) 
            {
                throw new WrongFoodException();
            }

            if (petService.ValidateFood(food.FoodType))
            {
                await petService.HungerDownAsync();
                await petService.MoodDownAsync();
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
