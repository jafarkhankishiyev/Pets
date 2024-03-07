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
    User UserToServe { get;  set; }

    Task AddPet(Pet pet);
    Task SetCash(double balance);
    Task AddFood(PetFood food);
    Task RemoveFood(PetFood food);
    Task<User> GetUser(Guid token);
}
