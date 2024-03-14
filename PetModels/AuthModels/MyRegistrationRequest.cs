namespace Models.AuthModels;

public record MyRegistrationRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public MyRegistrationRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
