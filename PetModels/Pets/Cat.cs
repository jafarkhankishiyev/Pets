using Pets.Models.Enumerations;

namespace Pets.Models.Pets;

public class Cat : Pet
{
    public Cat() { }

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
