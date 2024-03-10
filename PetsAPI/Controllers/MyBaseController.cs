using Microsoft.AspNetCore.Mvc;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBaseController : ControllerBase
    {
        protected string ConnectionString = "Server = localhost; User Id = postgres; Password = 123; Database=pets";
    }
}
