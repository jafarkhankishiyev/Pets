using Pets.Models.Enumerations;
using Pets.Models.Pets;

namespace CommonServices.DBServices.PetDB;

public interface IPetDBService
{
    public Pet PetToServe { get; set; }
    Task SetMood(MoodType mood);
    Task SetHunger(HungerType hunger);
}
