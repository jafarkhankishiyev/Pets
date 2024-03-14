using CommonServices.DBServices.PetDB;
using CommonServices.DBServices.UserDB;
using CommonServices.PetServices;
using Microsoft.AspNetCore.Mvc;
using Pets.Models;
using PetsAPI.Requests.PetRequests;

namespace PetsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetsController : MyBaseController
{
    private IPetDBService PetDB { get; set; }
    private IUserDBService UserDB { get; set; }

    public PetsController(IPetDBService petDB, IUserDBService userDB)
    {
        PetDB = petDB;
        UserDB = userDB;
        PetDB.SetDataSource(ConnectionString);
        UserDB.SetDataSource(ConnectionString);
    }

    //change petrequest to userid and petid

    [HttpPost("getpet")]
    public async Task<IActionResult> GetPet([FromBody] GetPetRequest getPetRequest)
    {
        var user = await UserDB.GetUserAsync(getPetRequest.Token);

        foreach (var pet in user.OwnedPets)
        {
            if (pet.Id == getPetRequest.PetId)
            {
                PetDB.PetToServe = pet;
                var petService = new PetServiceFactory(PetDB).GetAccordingPetService();

                await petService.GetPetAsync();
                return Ok();
            }
        }
        return BadRequest();
    }

    //change to food id and pet id

    [HttpPost("getfed")]
    public async Task<IActionResult> GetFed([FromBody] GetFedRequest getFedRequest)
    {
        var user = await UserDB.GetUserAsync(getFedRequest.Token);

        foreach (var pet in user.OwnedPets)
        {
            if (pet.Id == getFedRequest.PetId)
            {
                var petService = new PetServiceFactory(new PostgresPetDBService(ConnectionString, pet)).GetAccordingPetService();

                await petService.GetFedAsync(user, new PetFood(getFedRequest.FoodType));

                return Ok();
            }
        }

        return BadRequest();
    }
}
