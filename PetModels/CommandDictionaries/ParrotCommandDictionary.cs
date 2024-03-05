using Models.CommandPairs.ParrotPairs;
using Pets.Models.Enumerations.PetCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CommandDictionaries
{
    public static class ParrotCommandDictionary
    {
        public static Dictionary<ParrotCommands, string[]> CommandDictionary = new()
        {
            { ParrotCommands.Chat, [Chat.Command, Chat.Execution]},
            { ParrotCommands.Sing, [Sing.Command, Sing.Execution]},
            { ParrotCommands.Dead, [Dead.Command, Dead.Execution]}
        };
    }
}
