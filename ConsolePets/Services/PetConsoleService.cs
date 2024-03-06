using CommonServices;
using CommonServices.PetServices;
using Pets.Models.Pets;
using Pets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePets.Exceptions;

namespace ConsolePets.Services;

internal class PetConsoleService(PetService petService)
{
    PetService PetService { get; set; } = petService;

    public async Task PlayAsync()
    {
        await PetService.AffectByPlayOrCommandAsync();
        Console.Write($"\nYou and {PetService.PetToServe.Name} run around your backyard doing silly things and have fun!\n");
    }

    public async Task GetFedAsync(User UserToServe, PetFood food)
    {
        try
        {
            await PetService.GetFedAsync(UserToServe, food);
            var text = new StringBuilder("\nYour pet is fed! Its hunger level is now " + PetService.PetToServe.Hunger);
            text.Append("\nFood left:");
            foreach (PetFood p in UserToServe.OwnedFood)
            {
                text.Append("\n1. " + p.Name);
            }
            Console.Write(text.ToString());
        }
        catch
        {
            throw new WrongFoodException();
        }
    }

    public async Task GetPetAsync()
    {
        await petService.MoodUpAsync();
        StringBuilder text = new StringBuilder($"\nPetting {petService.PetToServe.Name}");
        text.Append($"\n{petService.PetToServe.Name}'s mood is now {petService.PetToServe.Mood}");
        Console.Write(text.ToString());
    }
}
