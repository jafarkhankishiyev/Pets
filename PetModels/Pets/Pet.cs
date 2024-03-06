using Pets.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Models.Pets
{
    public class Pet
    {
        //properties
        public int Id { get; set; }
        public string Name { get; set; }
        public FurType FurColor { get; set; }
        public MoodType Mood { get; set; }
        public HungerType Hunger { get; set; }

        //constructor
        public Pet()
        {
            Name = "None";
            FurColor = 0;
            Mood = 0;
            Hunger = 0;
        }

        public Pet(string name, FurType furColor)
        {
            Name = name;
            FurColor = furColor;
            Mood = MoodType.Ok;
            Hunger = HungerType.Ok;
        }
    }
}
