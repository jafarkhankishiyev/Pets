using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pets.Models;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBaseController : ControllerBase
    {
        protected string ConnectionString = "Server = localhost; User Id = postgres; Password = 123; Database=pets";
    }
}
