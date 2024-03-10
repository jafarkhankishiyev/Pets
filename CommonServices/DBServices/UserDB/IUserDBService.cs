using Pets.Models;
using Pets.Models.Pets;
using System.Data.Common;

namespace CommonServices.DBServices.UserDB;

public interface IUserDBService
{
    DbDataSource UserDataSource { get; }
    User UserToServe { get;  set; }

    Task AddPet(Pet pet);
    Task SetCash(double balance);
    Task AddFood(PetFood food);
    Task RemoveFood(PetFood food);
    Task<User> GetUser(Guid token);
    Task ValidateToken(Guid token);
}
