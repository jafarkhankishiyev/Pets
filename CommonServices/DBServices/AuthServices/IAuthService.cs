using Models.AuthModels;

namespace CommonServices.DBServices.AuthServices;

public interface IAuthService
{
    public Task<TokenResponse> LogIn(string username, string password);

    public Task<bool> Register(string username, string password);
}
