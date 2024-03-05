using CommonServices.DBServices.AuthServices;
using Npgsql;
using Pets.Models;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.UserDB;

public class PostgresUserDBService : IUserDBService
{
    private NpgsqlDataSource _dataSource { get; }

    internal User _user { get; }

    public PostgresUserDBService(string connectionString, string username, string password)
    {
        _dataSource = NpgsqlDataSource.Create(connectionString);
        _ = GetUser(username, password);
    }

    public void AddPet(Pet pet)
    {
        var addPetCommand = _dataSource.CreateCommand("INSERT INTO users_pets (user_id, pet_name, pet_hunger, pet_mood, pet_fur) " +
            $"VALUES ({_user.Id}, {pet.Name}, {(int)pet.Hunger}, {(int)pet.Mood}, {(int)pet.FurColor})");
    }

    public void AddFood(PetFood food)
    {
        throw new NotImplementedException();
    }

    public void RemoveFood(PetFood food)
    {
        throw new NotImplementedException();
    }

    public void SetCash(int balance)
    {
        throw new NotImplementedException();
    }

    public async Task GetUser(string username, string password)
    {
        var authService = new PostgresAuthService(_dataSource);

        var token = authService.LogIn(username, password);

        var getUserIdCommand = _dataSource.CreateCommand($"SELECT user_id FROM users_tokens WHERE token = {token}");
        var userIdReader = getUserIdCommand.ExecuteReader();

        var userId = 0;

        while (userIdReader.Read())
        {
            userId = userIdReader.GetInt32(0);
        }

        var getUserDataCommand = _dataSource.CreateCommand($"SELECT name, cash_balance FROM users WHERE id = {userId}; ");
        var getUserPetsCommand = _dataSource.CreateCommand($"SELECT pet_name, pet_hunger, pet_mood, pet_fur FROM users_pets WHERE user_id = {userId};");
        var getUserFoodsCommand = _dataSource.CreateCommand($"SELECT food_type FROM users_foods WHERE user_id = {userId}");
    }
}
