using CommonServices.DBServices;
using CommonServices.Exceptions;
using Models.AuthModels;
using Npgsql;
using Pets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.AuthServices;

public class PostgresAuthService(string connectionString) : IAuthService
{
    private NpgsqlDataSource AuthDataSource { get; } = NpgsqlDataSource.Create(connectionString);

    public async Task<TokenResponse> LogIn(string username, string password)
    {
        var command = AuthDataSource.CreateCommand("SELECT name FROM users WHERE name=@UserName");
        command.Parameters.AddWithValue("@UserName", username);

        using var reader = await command.ExecuteReaderAsync();

        var result = string.Empty;

        while (reader.Read())
        {
            result = reader.GetString(0);
        }

        if (string.IsNullOrWhiteSpace(result))
        {
            throw new WrongUsernameException();
        }

        var passwordCommand = AuthDataSource.CreateCommand("SELECT password FROM users WHERE name=@UserName");
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
            throw new WrongPasswordException();
        }

        var getUserIdCommand = AuthDataSource.CreateCommand("SELECT id FROM users WHERE name=@UserName AND password=@Password");
        getUserIdCommand.Parameters.AddWithValue("@UserName", username);
        getUserIdCommand.Parameters.AddWithValue("@Password", hashedInputPass.ToString());

        using var userIdReader = await getUserIdCommand.ExecuteReaderAsync();

        var userId = 0;

        while (userIdReader.Read())
        {
            userId = userIdReader.GetInt32(0);
        }

        var checkAvailableTokensCommand = AuthDataSource.CreateCommand($"SELECT token, valid_until FROM users_tokens WHERE user_id = {userId}");
        var availableTokensReader = await checkAvailableTokensCommand.ExecuteReaderAsync();

        var token = Guid.Empty;
        var validUntil = DateTime.MinValue;

        while (await availableTokensReader.ReadAsync()) 
        {
            if(availableTokensReader.GetDateTime(1) > DateTime.UtcNow)
            {
                token = availableTokensReader.GetGuid(0);
                validUntil = availableTokensReader.GetDateTime(1);
            }
            else
            {
                var deleteInvalidTokenCommand = AuthDataSource.CreateCommand($"DELETE FROM users_tokens WHERE valid_until='{availableTokensReader.GetDateTime(1)}'");
                await deleteInvalidTokenCommand.ExecuteNonQueryAsync();
            }
        }

        if(token != Guid.Empty)
        {
            return new TokenResponse(token, validUntil);
        }

        token = Guid.NewGuid();
        validUntil = DateTime.UtcNow.AddHours(5);

        var insertNewTokenCommand = AuthDataSource.CreateCommand($"INSERT INTO users_tokens (token, user_id, valid_until) VALUES ('{token}', {userId}, '{validUntil}')");

        await insertNewTokenCommand.ExecuteNonQueryAsync();
        
        return new TokenResponse(token, validUntil);
    }

    public async Task<bool> Register(string username, string password)
    {
        var checkUserNameCommand = AuthDataSource.CreateCommand("SELECT name FROM users WHERE name = @UserName");
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

        var addUserCommand = AuthDataSource.CreateCommand($"INSERT INTO users (name, password, cash_balance) VALUES (@UserName, '{hashedPass}', 500)");
        addUserCommand.Parameters.AddWithValue("@UserName", username);

        await addUserCommand.ExecuteNonQueryAsync();

        return true;
    }
}
