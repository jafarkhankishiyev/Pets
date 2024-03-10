using CommonServices.DBServices.PetDB;
using Models.CommandDictionaries;
using Pets.Models.Enumerations;

namespace CommonServices.PetServices;

public class DogService(IPetDBService petDB) : PetService(petDB)
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
