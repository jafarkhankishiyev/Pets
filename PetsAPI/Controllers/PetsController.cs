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
    [HttpPost("getpet")]
    public async Task<IActionResult> GetPet([FromBody] GetPetRequest getPetRequest)
    {
        var user = (await PostgresUserDBService.UserDBFactory(ConnectionString, getPetRequest.Token)).UserToServe;

        foreach (var pet in user.OwnedPets)
        {
            if (pet.Id == getPetRequest.PetId)
            {
                var petService = PetService.GetAccordingPetService(new PostgresPetDBService(ConnectionString, pet));

                await petService.GetPetAsync();

                return Ok();
            }
        }
        return BadRequest();
    }

    [HttpPost("getfed")]
    public async Task<IActionResult> GetFed([FromBody] GetFedRequest getFedRequest)
    {
        var user = (await PostgresUserDBService.UserDBFactory(ConnectionString, getFedRequest.Token)).UserToServe;

        foreach (var pet in user.OwnedPets)
        {
            if (pet.Id == getFedRequest.PetId)
            {
                var petService = PetService.GetAccordingPetService(new PostgresPetDBService(ConnectionString, pet));

                await petService.GetFedAsync(user, new PetFood(getFedRequest.FoodType));

                return Ok();
            }
        }

        return BadRequest();
    }
}
