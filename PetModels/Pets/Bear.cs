using Pets.Models.Enumerations;

namespace Pets.Models.Pets;

public class Bear : Pet
{
    public Bear() { }

    public Bear(string name, FurType furColor) : base(name, furColor)
    {
        Name = name;
        FurColor = furColor;
    }

    public override string ToString()
    {
        return "(Bear)";
    }
}
