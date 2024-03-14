using CommonServices.Exceptions;
using Npgsql;
using Pets.Models;
using Pets.Models.Enumerations;
using Pets.Models.Pets;
using System.Data.Common;

namespace CommonServices.DBServices.UserDB;

public class PostgresUserDBService : IUserDBService
{
    public DbDataSource UserDataSource { get; private set; }

    public User UserToServe { get; set; }

    public string ConnectionString { get; set; }

    public PostgresUserDBService(string connectionString)
    {
        UserDataSource = NpgsqlDataSource.Create(connectionString);
    }

    public PostgresUserDBService()
    {

    }

    public void SetDataSource(string connectionString)
    {
        UserDataSource = NpgsqlDataSource.Create(connectionString);
    }

    public async Task AddPetAsync(Pet pet/*, int userId*/)
    {
        var petType = pet switch
        {
            Bear => 1,
            Cat => 2,
            Dog => 3,
            Parrot => 4,
            _ => 0,
        };

        var addPetCommand = UserDataSource.CreateCommand($"INSERT INTO users_pets (user_id, pet_name, pet_hunger, pet_mood, pet_fur, pet_type) VALUES ({/*userId*/UserToServe.Id}, '{pet.Name}', {(int)pet.Hunger}, {(int)pet.Mood}, {(int)pet.FurColor}, {petType})");

        await addPetCommand.ExecuteNonQueryAsync();
    }

    public async Task AddFoodAsync(PetFood food)
    {
        var addFoodCommand = UserDataSource.CreateCommand($"INSERT INTO users_foods (user_id, food_type) VALUES ({UserToServe.Id}, {(int)food.FoodType})");

        await addFoodCommand.ExecuteNonQueryAsync();
    }

    public async Task RemoveFoodAsync(PetFood food)
    {
        var getOneItemToRemoveCommand = UserDataSource.CreateCommand($"SELECT id FROM users_foods WHERE " +
            $"user_id = {UserToServe.Id} AND food_type = {food.FoodType} LIMIT 1");

        using var getOneItemToRemoveReader = await getOneItemToRemoveCommand.ExecuteReaderAsync();

        var removeFoodCommand = UserDataSource.CreateCommand($"DELETE FROM users_foods WHERE id = {getOneItemToRemoveReader.GetInt32(0)}");
    }

    public async Task SetCashAsync(double balance)
    {
        var setCashCommand = UserDataSource.CreateCommand($"UPDATE users SET cash_balance = {balance} WHERE id = {UserToServe.Id}");

        await setCashCommand.ExecuteNonQueryAsync();
    }

    public async Task TopUpCash(double amount, string description)
    {
        var topUpCommand = UserDataSource.CreateCommand($"INSERT INTO users_cash (user_id, top_up, withdrawal, description) VALUES ({UserToServe.Id}, {amount}, 0, '{description}')");

        await topUpCommand.ExecuteNonQueryAsync();
    }

    public async Task WithdrawCash(double amount, string description)
    {
        var withdrawalCommand = UserDataSource.CreateCommand($"INSERT INTO users_cash (user_id, top_up, withdrawal, description) VALUES ({UserToServe.Id}, 0, {amount}, '{description}')");

        await withdrawalCommand.ExecuteNonQueryAsync();
    }

    public async Task<User> GetUserAsync(Guid token)
    {
        var byteArr = token.ToByteArray();
        var userId = (int)byteArr[15];

        var getUserDataCommand = UserDataSource.CreateCommand($"SELECT name FROM users WHERE id = {userId}; ");
        var getUserCashCommand = UserDataSource.CreateCommand($"SELECT top_up, withdrawal FROM users_cash WHERE user_id = {userId}");
        var getUserPetsCommand = UserDataSource.CreateCommand($"SELECT pet_name, pet_hunger, pet_mood, pet_fur, id, pet_type FROM users_pets WHERE user_id = {userId};");
        var getUserFoodsCommand = UserDataSource.CreateCommand($"SELECT food_type FROM users_foods WHERE user_id = {userId}");

        using var getUserDataReader = await getUserDataCommand.ExecuteReaderAsync();

        var user = new User(string.Empty);

        user.Id = userId;

        while(await getUserDataReader.ReadAsync()) 
        {
            user.Username = getUserDataReader.GetString(0);
        }

        using var getUserCashReader = await getUserCashCommand.ExecuteReaderAsync();

        while(await getUserCashReader.ReadAsync())
        {
            user.CashBalance += getUserCashReader.GetDouble(0);
            user.CashBalance -= getUserCashReader.GetDouble(1);
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

    public async Task ValidateTokenAsync(Guid token)
    {
        var validateTokenCommand = UserDataSource.CreateCommand($"SELECT valid_until FROM users_tokens WHERE token = '{token}' AND valid_until >= '{DateTime.UtcNow}';");

        var validateTokenReader = await validateTokenCommand.ExecuteReaderAsync();

        var valid = false;

        while (await validateTokenReader.ReadAsync()) 
        {
            valid = true;
        }

        if(!valid)
        {
            throw new InvalidTokenException();
        }
    }
}
