using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.DBService;

public interface IPetDBService
{
    void MoodUp();
    void MoodDown();
    void HungerUp();
    void HungerDown();
    void GetFed();
}
