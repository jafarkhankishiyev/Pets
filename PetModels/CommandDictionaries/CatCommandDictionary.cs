using Models.CommandPairs.CatPairs;
using Pets.Models.Enumerations.PetCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CommandDictionaries
{
    public static class CatCommandDictionary
    {
        public static Dictionary<CatCommands, string[]> CommandDictionary = new()
        {
            { CatCommands.Meow, [Meow.Command, Meow.Execution] }
        };
    }
}
