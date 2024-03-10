
namespace Models.AuthModels;

public class TokenResponse
{
    public Guid Token { get; }
    public DateTime ValidUntil { get; }
    public TokenResponse(Guid token, DateTime validUntil)
    {
        Token = token;
        ValidUntil = validUntil;
    }
}
