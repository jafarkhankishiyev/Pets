using Pets.Models;
using Pets.Models.Pets;
using System.Data.Common;

namespace CommonServices.DBServices.UserDB;

public interface IUserDBService
{
    public DbDataSource UserDataSource { get; }
    public User UserToServe { get;  set; }
    public string ConnectionString { get; set; }

    Task AddPetAsync(Pet pet);
    Task SetCashAsync(double balance);
    Task AddFoodAsync(PetFood food);
    Task RemoveFoodAsync(PetFood food);
    Task<User> GetUserAsync(Guid token);
    Task ValidateTokenAsync(Guid token);
    Task TopUpCash(double amount, string description);
    Task WithdrawCash(double amount, string description);
    void SetDataSource(string connectionString);
}
