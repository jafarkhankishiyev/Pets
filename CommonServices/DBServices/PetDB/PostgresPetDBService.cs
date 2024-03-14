using Npgsql;
using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.DBServices.PetDB;

public class PostgresPetDBService : IPetDBService
{
    private NpgsqlDataSource PetDataSource;

    public Pet PetToServe { get; set; }

    public string ConnectionString { get; set; }

    public PostgresPetDBService(string connectionString, Pet pet)
    {
        PetDataSource = NpgsqlDataSource.Create(connectionString);
        PetToServe = pet;
    }

    public PostgresPetDBService() 
    {
    }

    public async Task SetHungerAsync(HungerType hunger)
    {
        var setHungerCommand =  PetDataSource.CreateCommand($"UPDATE users_pets SET pet_hunger = {(int)hunger} WHERE id = {PetToServe.Id}");

        await setHungerCommand.ExecuteNonQueryAsync();
    }

    public async Task SetMoodAsync(MoodType mood)
    {
        var setMoodCommand = PetDataSource.CreateCommand($"UPDATE users_pets SET pet_mood = {(int)mood} WHERE id = {PetToServe.Id}");

        await setMoodCommand.ExecuteNonQueryAsync();
    }

    public void SetDataSource(string connectionString)
    {
        PetDataSource = NpgsqlDataSource.Create(connectionString);
    }
}
