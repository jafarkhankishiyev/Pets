using Pets.Models.Enumerations;

namespace Pets.Models.Pets;

public class Parrot : Pet
{
    public Parrot() { }

    public Parrot(string name, FurType furColor) : base(name, furColor)
    {
        Name = name;
        FurColor = furColor;
    }

    public override string ToString()
    {
        return "(Parrot)";
    }
}
