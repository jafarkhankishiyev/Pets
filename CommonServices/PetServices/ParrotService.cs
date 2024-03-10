using CommonServices.DBServices.PetDB;
using Models.CommandDictionaries;
using Pets.Models.Enumerations;

namespace CommonServices.PetServices;

public class ParrotService(IPetDBService petDB) : PetService(petDB)
{
    public override string[] GetCommands()
    {
        string[] parrotCommandDescriptions = [];
        foreach (var command in ParrotCommandDictionary.CommandDictionary) 
        {
            Array.Resize(ref parrotCommandDescriptions, parrotCommandDescriptions.Length + 1);
            parrotCommandDescriptions[^1] = command.Value[0];
        }
        return parrotCommandDescriptions;
    }

    public override string[] GetCommandExecution()
    {
        string[] parrotCommandExecutions = [];
        foreach (var command in ParrotCommandDictionary.CommandDictionary)
        {
            Array.Resize(ref parrotCommandExecutions, parrotCommandExecutions.Length + 1);
            parrotCommandExecutions[^1] = command.Value[0];
        }
        return parrotCommandExecutions;
    }

    public override bool ValidateFood(FoodType f)
    {
        return f == FoodType.ParrotFood;
    }
}
