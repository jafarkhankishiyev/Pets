using Models.CommandDictionaries;
using Pets.Models.Enumerations;
using Pets.Models.Enumerations.PetCommands;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.PetServices
{
    public class DogService(Pet pet) : PetService(pet)
    {
        public override string[] GetCommands()
        {
            string[] dogCommandDescriptions = [];
            foreach (var command in DogCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref dogCommandDescriptions, dogCommandDescriptions.Length + 1);
                dogCommandDescriptions[^1] = command.Value[0];
            }
            return dogCommandDescriptions;
        }

        public override string[] GetCommandExecution()
        {
            string[] dogCommandDescriptions = [];
            foreach (var command in DogCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref dogCommandDescriptions, dogCommandDescriptions.Length + 1);
                dogCommandDescriptions[^1] = command.Value[0];
            }
            return dogCommandDescriptions;
        }

        public override bool ValidateFood(FoodType f)
        {
            if (f == FoodType.DogFood)
                return true;
            return false;
        }
    }
}
