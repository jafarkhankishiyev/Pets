using CommonServices.DBServices;
using Models.AuthModels;
using Npgsql.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBServices.AuthServices;

public interface IAuthService
{
    public Task<TokenResponse> LogIn(string username, string password);

    public Task<bool> Register(string username, string password);
}
