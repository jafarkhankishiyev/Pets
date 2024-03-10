using Models.CommandPairs.BearPairs;
using Pets.Models.Enumerations.PetCommands;

namespace Models.CommandDictionaries;

public static class BearCommandDictionary
{
    public static Dictionary<BearCommands, string[]> CommandDictionary = new()
    {
        { BearCommands.Monocycle, [Monocycle.Command, Monocycle.Execution] },
        { BearCommands.Forest, [Forest.Command, Forest.Execution] },
        { BearCommands.Trash, [Trash.Command, Trash.Execution] }
    };
}
