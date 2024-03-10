using CommonServices;
using CommonServices.DBServices.PetDB;
using CommonServices.DBServices.UserDB;
using CommonServices.PetServices;
using Microsoft.AspNetCore.Mvc;
using Pets.Models;
using Pets.Models.Pets;
using PetsAPI.Requests.UserRequests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyBaseController
    {
        [HttpGet("{token}")]
        public async Task<User> GetUser(Guid token)
        {
            var userDB = await PostgresUserDBService.UserDBFactory(ConnectionString, token);

            return userDB.UserToServe;
        }

        [HttpGet("work/{token}")]
        public async Task<double> Work(Guid token)
        {
            var userService = await GetUserService(token);

            await userService.WorkAsync();

            return userService.UserToServe.CashBalance;
        }

        [HttpPost("addfood")]
        public async Task<IActionResult> AddFood([FromBody]AddFoodRequest addFoodRequest)
        {
            var userDB = await PostgresUserDBService.UserDBFactory(ConnectionString, addFoodRequest.Token);
            var userService = new UserService(userDB);
            await userService.BuyFoodAsync(addFoodRequest.FoodType);

            return Ok(userService.UserToServe.OwnedFood);
        }

        [HttpPost("addpet")]
        public async Task<IActionResult> AddPet([FromBody]AddPetRequest addPetRequest)
        {
            var userService = await GetUserService(addPetRequest.Token);
            var pet = GetAccordingPet(addPetRequest.PetType, new Pet(addPetRequest.PetName, addPetRequest.FurColor));
            var added = await userService.AddPetAsync(pet);
            
            if(added)
            {
                return Ok(added);
            }

            return BadRequest();
        }

        [HttpPost("command")]
        public async Task<IActionResult> Command([FromBody] CommandRequest commandRequest)
        {
            var userService = await GetUserService(commandRequest.Token);

            foreach(var pet in userService.UserToServe.OwnedPets)
            {
                if (pet.Id == commandRequest.PetId)
                {
                    var petService = PetService.GetAccordingPetService(new PostgresPetDBService(ConnectionString, pet));

                    var petFood = await userService.CommandAsync(petService, commandRequest.Command);
                    
                    if (petFood != null)
                    {
                        return Ok(petFood);
                    }

                    return Ok();
                }
            }

            return BadRequest();
        }

        private async Task<UserService> GetUserService(Guid token)
        {
            var userDB = await PostgresUserDBService.UserDBFactory(ConnectionString, token);

            return new UserService(userDB);
        }

        private Pet GetAccordingPet(int petType, Pet pet)
        {
            switch (petType)
            {
                case 1:
                    pet = new Bear(pet.Name, pet.FurColor);
                    break;
                case 2:
                    pet = new Cat(pet.Name, pet.FurColor);
                    break;
                case 3:
                    pet = new Dog(pet.Name, pet.FurColor);
                    break;
                case 4:
                    pet = new Parrot(pet.Name, pet.FurColor);
                    break;
                default:
                    _ = new Pet();
                    break;
            }
            return pet;
        }
    }
}
