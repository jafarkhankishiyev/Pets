using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.UserDB;

public interface IUserDBService
{
    void AddPet(Pet pet);
    void SetCash(int balance);
    void AddFood(PetFood food);
    void RemoveFood(PetFood food);
}
