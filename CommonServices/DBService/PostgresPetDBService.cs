using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBService
{
    public class PostgresPetDBService : IPetDBService
    {
        private NpgsqlDataSource _dataSource;
        public PostgresPetDBService(string connectionString) 
        {
            _dataSource = NpgsqlDataSource.Create(connectionString);
        }
        public void GetFed()
        {
            throw new NotImplementedException();
        }

        public void HungerDown()
        {
            throw new NotImplementedException();
        }

        public void HungerUp()
        {
            throw new NotImplementedException();
        }

        public void MoodDown()
        {
            throw new NotImplementedException();
        }

        public void MoodUp()
        {
            throw new NotImplementedException();
        }
    }
}
