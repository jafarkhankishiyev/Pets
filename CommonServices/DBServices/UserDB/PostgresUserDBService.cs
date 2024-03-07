using CommonServices.DBServices.AuthServices;
using Npgsql;
using Pets.Models;
using Pets.Models.Enumerations;
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
    private NpgsqlDataSource UserDataSource { get; }

    public User UserToServe { get; set; }

    public PostgresUserDBService(string connectionString)
    {
        UserDataSource = NpgsqlDataSource.Create(connectionString);
    }

    public async Task AddPet(Pet pet)
    {
        var petType = 0;

        switch(pet)
        {
            case Bear:
                petType = 1;
                break;
            case Cat: 
                petType = 2; 
                break;
            case Dog: 
                petType = 3; 
                break;
            case Parrot: 
                petType = 4; 
                break;
            default:
                petType = 0;
                break;
        }
        var addPetCommand = UserDataSource.CreateCommand($"INSERT INTO users_pets (user_id, pet_name, pet_hunger, pet_mood, pet_fur, pet_type) VALUES ({UserToServe.Id}, '{pet.Name}', {(int)pet.Hunger}, {(int)pet.Mood}, {(int)pet.FurColor}, {petType})");

        await addPetCommand.ExecuteNonQueryAsync();
    }

    public async Task AddFood(PetFood food)
    {
        var addFoodCommand = UserDataSource.CreateCommand($"INSERT INTO users_foods (user_id, food_type) VALUES ({UserToServe.Id}, {(int)food.FoodType})");

        await addFoodCommand.ExecuteNonQueryAsync();
    }

    public async Task RemoveFood(PetFood food)
    {
        var getOneItemToRemoveCommand = UserDataSource.CreateCommand($"SELECT id FROM users_foods WHERE " +
            $"user_id = {UserToServe.Id} AND food_type = {food.FoodType} LIMIT 1");

        using var getOneItemToRemoveReader = await getOneItemToRemoveCommand.ExecuteReaderAsync();

        var removeFoodCommand = UserDataSource.CreateCommand($"DELETE FROM users_foods WHERE id = {getOneItemToRemoveReader.GetInt32(0)}");
    }

    public async Task SetCash(double balance)
    {
        var setCashCommand = UserDataSource.CreateCommand($"UPDATE users SET cash_balance = {balance} WHERE id = {UserToServe.Id}");
        await setCashCommand.ExecuteNonQueryAsync();
    }

    public async Task<User> GetUser(Guid token)
    {
        var getUserIdCommand = UserDataSource.CreateCommand($"SELECT user_id FROM users_tokens WHERE token = '{token}'");
        var userIdReader = await getUserIdCommand.ExecuteReaderAsync();

        var userId = 0;

        while(await userIdReader.ReadAsync())
        {
            userId = userIdReader.GetInt32(0);
        }

        var getUserDataCommand = UserDataSource.CreateCommand($"SELECT name, cash_balance FROM users WHERE id = {userId}; ");
        var getUserPetsCommand = UserDataSource.CreateCommand($"SELECT pet_name, pet_hunger, pet_mood, pet_fur, id, pet_type FROM users_pets WHERE user_id = {userId};");
        var getUserFoodsCommand = UserDataSource.CreateCommand($"SELECT food_type FROM users_foods WHERE user_id = {userId}");

        using var getUserDataReader = await getUserDataCommand.ExecuteReaderAsync();

        var user = new User(string.Empty);

        user.Id = userId;

        while(await getUserDataReader.ReadAsync()) 
        {
            user.UserName = getUserDataReader.GetString(0);
            user.CashBalance = getUserDataReader.GetDouble(1);
        }

        using var getUserPetsReader = await getUserPetsCommand.ExecuteReaderAsync();

        var ownedPets = new List<Pet>();

        while (await getUserPetsReader.ReadAsync())
        {
            var pet = new Pet();

            switch (getUserPetsReader.GetInt32(5))
            {
                case 1:
                    pet = new Bear(); 
                    break;
                case 2:
                    pet = new Cat();
                    break;
                case 3:
                    pet = new Dog();
                    break;
                case 4:
                    pet = new Parrot();
                    break;
                default:
                    break;
            }

            pet.Hunger = (HungerType)getUserPetsReader.GetInt32(1);
            pet.Mood = (MoodType)getUserPetsReader.GetInt32(2);
            pet.Id = getUserPetsReader.GetInt32(4);
            pet.Name = getUserPetsReader.GetString(0);
            pet.FurColor = (FurType)getUserPetsReader.GetInt32(3);

            ownedPets.Add(pet);
        }

        user.OwnedPets = ownedPets.ToArray();

        using var getUserFoodsReader = await getUserFoodsCommand.ExecuteReaderAsync();

        var ownedFood = new List<PetFood>();

        while(await getUserFoodsReader.ReadAsync())
        {
            ownedFood.Add(new PetFood((FoodType)getUserFoodsReader.GetInt32(0)));
        }

        user.OwnedFood = ownedFood.ToArray();
        return user;
    }

    public static async Task<PostgresUserDBService> UserDBFactory(string connectionString, Guid token)
    {
        var userDB = new PostgresUserDBService(connectionString);
        var user = await userDB.GetUser(token); 

        userDB.UserToServe = user;

        return userDB;
    }
}
