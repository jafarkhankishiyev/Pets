﻿using Pets.Models.Enumerations;
using Pets.Models.Enumerations.PetCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Models.Pets
{
    public class Cat : Pet
    {
        public Cat(string name, FurType furColor) : base(name, furColor)
        {
            Name = name;
            FurColor = furColor;
        }

        public override string ToString()
        {
            return "(Cat)";
        }
    }
}
