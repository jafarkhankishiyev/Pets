using Pets.Models.Enumerations;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.PetDB;

public interface IPetDBService
{
    public Pet PetToServe { get; set; }
    Task SetMood(MoodType mood);
    Task SetHunger(HungerType hunger);
}
