using Npgsql;
using Pets.Models;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBService;

public class PostgresUserDBService : IUserDBService
{
    private NpgsqlDataSource _dataSource { get; }

    private User user { get; }

    public PostgresUserDBService (string connectionString)
    {
       _dataSource = NpgsqlDataSource.Create(connectionString);
    }

    public void AddPet(Pet pet)
    {
        throw new NotImplementedException();
    }

    public void Work()
    {
        throw new NotImplementedException();
    }

    public void Command()
    {
        throw new NotImplementedException();
    }

    public void BuyFood()
    {
        throw new NotImplementedException();
    }
}
