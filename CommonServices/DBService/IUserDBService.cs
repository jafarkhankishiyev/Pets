using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBService;

public interface IUserDBService
{
    void AddPet(Pet pet);
    void Work();
    void Command();
    void BuyFood();
}
