using CommonServices.DBServices.PetDB;
using Models.CommandDictionaries;
using Pets.Models.Enumerations;
using Pets.Models.Enumerations.PetCommands;
using Pets.Models.Pets;
using System.ComponentModel;

namespace CommonServices.PetServices
{
    public class BearService(IPetDBService petDB) : PetService(petDB)
    {
        public override string[] GetCommands()
        {
            string[] bearCommandDescriptions = [];
            foreach (var command in BearCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref bearCommandDescriptions, bearCommandDescriptions.Length + 1);
                bearCommandDescriptions[^1] = command.Value[0];
            }
            return bearCommandDescriptions;
        }

        public override string[] GetCommandExecution()
        {
            string[] bearCommandDescriptions = [];
            foreach (var command in BearCommandDictionary.CommandDictionary)
            {
                Array.Resize(ref bearCommandDescriptions, bearCommandDescriptions.Length + 1);
                bearCommandDescriptions[^1] = command.Value[0];
            }
            return bearCommandDescriptions;
        }

        public override bool ValidateFood(FoodType f)
        {
            if (f == FoodType.BearFood)
                return true;
            return false;
        }
    }
}
