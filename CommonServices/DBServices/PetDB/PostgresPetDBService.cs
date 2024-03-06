using Npgsql;
using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.PetDB;

public class PostgresPetDBService : IPetDBService
{
    private NpgsqlDataSource PetDataSource;

    internal Pet PetToServe;

    public PostgresPetDBService(string connectionString, Pet pet)
    {
        PetDataSource = NpgsqlDataSource.Create(connectionString);
        PetToServe = pet;
    }

    public async Task SetHunger(HungerType hunger)
    {
        var setHungerCommand =  PetDataSource.CreateCommand($"UPDATE users_pets SET pet_hunger = {(int)hunger} WHERE id = {PetToServe.Id}");

        await setHungerCommand.ExecuteNonQueryAsync();
    }

    public async Task SetMood(MoodType mood)
    {
        var setMoodCommand = PetDataSource.CreateCommand($"UPDATE users_pets SET pet_mood = {(int)mood} WHERE id = {PetToServe.Id}");

        await setMoodCommand.ExecuteNonQueryAsync();
    }

}
