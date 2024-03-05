using CommonServices.DBServices;
using Npgsql.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.AuthServices;

internal interface IAuthService
{
    public Task<Guid> LogIn(string username, string password);

    public Task<bool> Register(string username, string password);
}
