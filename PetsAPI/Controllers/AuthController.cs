using Microsoft.AspNetCore.Mvc;
using CommonServices.DBServices.AuthServices;
using Models.AuthModels;

namespace PetsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : MyBaseController
{
    private PostgresAuthService ControllerAuthService;
    public AuthController() 
    {
        ControllerAuthService = new PostgresAuthService(ConnectionString);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] MyLoginRequest loginRequest)
    {
        var tokenResponse = await ControllerAuthService.LogIn(loginRequest.Username, loginRequest.Password);
        return Ok(tokenResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] MyRegistrationRequest registrationRequest)
    {
        var registered = await ControllerAuthService.Register(registrationRequest.Username, registrationRequest.Password);
        
        if(registered)
        {
            return Ok(registered);
        }

        return BadRequest();
    } 
}
