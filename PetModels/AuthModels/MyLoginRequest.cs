namespace Models.AuthModels;

public class MyLoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public MyLoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
