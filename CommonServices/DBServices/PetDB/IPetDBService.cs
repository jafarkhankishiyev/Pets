using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.DBServices.PetDB;

public interface IPetDBService
{
    public Pet PetToServe { get; set; }
    Task SetMoodAsync(MoodType mood);
    Task SetHungerAsync(HungerType hunger);
    void SetDataSource(string connectionString);
}
