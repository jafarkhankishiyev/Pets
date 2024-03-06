using CommonServices.DBServices.PetDB;
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
    public class CatService(Pet pet, IPetDBService petDB) : PetService(pet, petDB)
    {
        public override string[] GetCommands()
        {
            string[] catCommandDescriptions = [];
            foreach (var command in CatCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref catCommandDescriptions, catCommandDescriptions.Length + 1);
                catCommandDescriptions[^1] = command.Value[0];
            }
            return catCommandDescriptions;
        }

        public override string[] GetCommandExecution()
        {
            string[] catCommandDescriptions = [];
            foreach (var command in CatCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref catCommandDescriptions, catCommandDescriptions.Length + 1);
                catCommandDescriptions[^1] = command.Value[0];
            }
            return catCommandDescriptions;
        }

        public override bool ValidateFood(FoodType f)
        {
            if (f == FoodType.CatFood)
                return true;
            return false;
        }
    }
}
