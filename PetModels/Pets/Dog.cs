using Pets.Models.Enumerations;

namespace Pets.Models.Pets;

public class Dog : Pet
{
    public Dog() { }

    public Dog(string name, FurType furColor) : base(name, furColor)
    {
        Name = name;
        FurColor = furColor;
    }

    public override string ToString()
    {
        return "(Dog)";
    }
}
