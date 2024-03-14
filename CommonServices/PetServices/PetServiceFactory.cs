using CommonServices.DBServices.PetDB;
using Pets.Models.Pets;

namespace CommonServices.PetServices
{
    public class PetServiceFactory
    {
        private IPetDBService PetDB { get; init; }  
        public PetServiceFactory(IPetDBService petDB)
        {
            PetDB = petDB;
        }
        public PetService GetAccordingPetService()
        {
            PetService petService = PetDB.PetToServe switch
            {
                Bear => new BearService(PetDB),
                Cat => new CatService(PetDB),
                Dog => new DogService(PetDB),
                Parrot => new ParrotService(PetDB),
                _ => throw new Exception(),
            };

            return petService;
        }
    }
}
