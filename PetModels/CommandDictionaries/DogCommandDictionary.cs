using Models.CommandPairs.DogPairs;
using Pets.Models.Enumerations.PetCommands;

namespace Models.CommandDictionaries;

public static class DogCommandDictionary
{
    public static Dictionary<DogCommands, string[]> CommandDictionary = new()
    {
        { DogCommands.Bark, [Bark.Command, Bark.Execution] },
        { DogCommands.Stick, [Stick.Command, Stick.Execution] },
        { DogCommands.Come, [Come.Command, Come.Execution] },
        { DogCommands.Handshake, [Handshake.Command, Handshake.Execution] }
    };
}
