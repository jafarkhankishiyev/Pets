using Models.CommandPairs.BearPairs;
using Pets.Models.Enumerations.PetCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CommandDictionaries
{
    public static class BearCommandDictionary
    {
        public static Dictionary<BearCommands, string[]> CommandDictionary = new()
        {
            { BearCommands.Monocycle, [Monocycle.Command, Monocycle.Execution] },
            { BearCommands.Forest, [Forest.Command, Forest.Execution] },
            { BearCommands.Trash, [Trash.Command, Trash.Execution] }
        };
    }
}
