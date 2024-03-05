using CommonServices.DBServices;
using CommonServices.Exceptions;
using Npgsql;
using Pets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.AuthServices;

internal class PostgresAuthService(NpgsqlDataSource dataSource) : IAuthService
{
    private NpgsqlDataSource _dataSource { get; } = dataSource;

    public async Task<Guid> LogIn(string username, string password)
    {
        var command = _dataSource.CreateCommand("SELECT name FROM users WHERE name=@UserName;");
        command.Parameters.AddWithValue("@UserName", username);

        using var reader = await command.ExecuteReaderAsync();

        var result = string.Empty;

        while (reader.Read())
        {
            result = reader.GetString(0);
        }

        if (result == string.Empty)
        {
            throw new WrongUsernameException();
        }

        var passwordCommand = _dataSource.CreateCommand("SELECT password FROM users WHERE name=@UserName");
        passwordCommand.Parameters.AddWithValue("@UserName", username);

        using var passwordReader = await passwordCommand.ExecuteReaderAsync();

        var realPass = string.Empty;

        while (passwordReader.Read())
        {
            realPass = passwordReader.GetString(0);
        }

        var inputPassToHash = Encoding.UTF8.GetBytes(password);
        var hashedInputPassArr = MD5.HashData(inputPassToHash);

        var hashedInputPass = new StringBuilder();

        foreach(var hashedByte in hashedInputPassArr) 
        {
            hashedInputPass.Append(hashedByte.ToString("x2").ToLower());
        }

        if(hashedInputPass.ToString() != realPass)
        {
            throw new WrongUsernameException();
        }

        var getUserIdCommand = _dataSource.CreateCommand("SELECT id FROM users WHERE name=@UserName AND password=@Password");
        using var userIdReader = await getUserIdCommand.ExecuteReaderAsync();

        var userId = 0;

        while (userIdReader.Read())
        {
            userId = userIdReader.GetInt32(0);
        }

        var token = new Guid();

        var insertNewTokenCommand = _dataSource.CreateCommand($"INSERT INTO users_tokens (token, user_id, valid_until) " +
            $"VALUES ({token}, {userId}, {DateTime.UtcNow.AddHours(5)})");
        
        return token;
    }

    public async Task<bool> Register(string username, string password)
    {
        var checkUserNameCommand = _dataSource.CreateCommand("SELECT name FROM users WHERE name = @UserName");
        checkUserNameCommand.Parameters.AddWithValue("@UserName", username);

        using var userNameReader = await checkUserNameCommand.ExecuteReaderAsync();

        var result = string.Empty;

        while (userNameReader.Read())
        {
            result = userNameReader.GetString(0);
        }

        if(result != string.Empty)
        {
            throw new TakenUsernameException();
        }

        var hashedPassArr = MD5.HashData(Encoding.UTF8.GetBytes(password));
        var hashedPass = new StringBuilder();

        foreach (var passByte in hashedPassArr) 
        {
            hashedPass.Append(passByte.ToString("x2").ToLower());
        }

        var addUserCommand = _dataSource.CreateCommand($"INSERT INTO users (name, password, cash_balance) " +
            $"VALUES (@UserName, {hashedPass}, 500)");
        addUserCommand.Parameters.AddWithValue("@UserName", username);

        return true;
    }
}
