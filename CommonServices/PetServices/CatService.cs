using CommonServices.DBServices.PetDB;
using Models.CommandDictionaries;
using Pets.Models.Enumerations;

namespace CommonServices.PetServices;

public class CatService(IPetDBService petDB) : PetService(petDB)
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
