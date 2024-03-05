using Npgsql;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.PetDB;

public class PostgresPetDBService : IPetDBService
{
    private NpgsqlDataSource _dataSource;
    private Pet _pet;
    public PostgresPetDBService(string connectionString, Pet pet)
    {
        _dataSource = NpgsqlDataSource.Create(connectionString);
        _pet = pet;
    }

    public void SetHunger(int hunger)
    {
        throw new NotImplementedException();
    }

    public void SetHunger()
    {
        throw new NotImplementedException();
    }

    public void SetMood()
    {
        throw new NotImplementedException();
    }
}
