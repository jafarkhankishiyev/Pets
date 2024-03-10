using Models.CommandPairs.CatPairs;
using Pets.Models.Enumerations.PetCommands;

namespace Models.CommandDictionaries;

public static class CatCommandDictionary
{
    public static Dictionary<CatCommands, string[]> CommandDictionary = new()
    {
        { CatCommands.Meow, [Meow.Command, Meow.Execution] }
    };
}
