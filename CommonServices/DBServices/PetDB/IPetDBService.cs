using Pets.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.PetDB;

public interface IPetDBService
{
    Task SetMood(MoodType mood);
    Task SetHunger(HungerType hunger);
}
