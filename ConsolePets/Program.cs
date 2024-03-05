
using CommonServices.PetServices;
using CommonServices;
using Pets.Models.Pets;
using Pets.Models;
using ConsolePets.Services;
using ConsolePets.Exceptions;

string uName = string.Empty;
while (string.IsNullOrEmpty(uName))
{
    Console.Write("Welcome, dear fellow! What's your name?\n");
    uName = Console.ReadLine();
}
User user = new(uName);
UserConsoleService userConsoleService = new(new UserService(user));
bool petAdded = false;
while (!petAdded)
{
    Console.Write($"Nice to meet you, {user.UserName}!\n");
    petAdded = userConsoleService.AddPet();
}
bool exitOption = false;
Console.Clear();
while (!exitOption)
{
    Console.Write("\n\nGreat! Now you can decide what to do, provide number: \n1. Feed, command, pet, and play with pets\n2. View current pets, balance, and food. \n3. Add pets\n4. Go to work\n5. Go shopping\n");
    string answerString = Console.ReadLine();
    if (Int32.TryParse(answerString, out int value))
    {
        if (value > 0 && value < 6)
        {
            if (value == 1)
            {
                GetInteractionOptions();
            }
            else if (value == 2)
            {
                ViewCurrent();
            }
            else if (value == 3)
            {
                Console.Write("\n Great! Let's add another pet");
                petAdded = userConsoleService.AddPet();
                foreach (Pet pet in user.OwnedPets)
                {
                    Console.Write(pet.Name + "\n");
                }
            }
            else if (value == 4)
            {
                userConsoleService.Work();
            }
            else if (value == 5)
            {
                userConsoleService.BuyFood();
            }
        }
        else
        {
            throw new NoSuchOptionException();
        }
    }
    else
    {
        throw new NoSuchOptionException();
    }
}
void FeedPet(int val)
{
    if (user.OwnedFood.Length > 0)
    {
        Console.Write("\nOkay. Choose the food, provide a number:\n");
        int i = 1;
        foreach (PetFood f in user.OwnedFood)
        {
            Console.Write(i + ". " + f.Name + "\n");
            i++;
        }
        string foodAnswerStr = Console.ReadLine();
        if (Int32.TryParse(foodAnswerStr, out int foodAnswer))
        {
            if (foodAnswer > 0 && foodAnswer <= user.OwnedFood.Length)
            {
                PetConsoleService petConsoleService = new(PetService.GetAccordingPetService(user.OwnedPets[val - 1]));
                petConsoleService.GetFed(user, user.OwnedFood[foodAnswer - 1]);
            }
            else
                throw new NoSuchOptionException();
        }
        else
        {
            throw new NoSuchOptionException();
        }
    }
    else
    {
        throw new NoFoodException();
    }
}

void CommandPet(int val)
{
    userConsoleService.Command(user.OwnedPets[val - 1]);
}

void PetPet(int val)
{
    PetConsoleService petConsoleService = new(PetService.GetAccordingPetService(user.OwnedPets[val - 1]));
    petConsoleService.GetPet();
}

void ViewCurrent()
{
    Console.Write("\nCurrent Pets:\n");
    foreach (Pet p in user.OwnedPets)
    {
        Console.Write($"\n\n{p.Name} {p}\nMood: {p.Hunger}\nHunger: {p.Hunger}");
    }
    Console.Write($"\n\nCurrent balance: ${user.CashBalance}");
    Console.Write("\n\nCurret foods:\n");
    int i = 1;
    foreach (PetFood f in user.OwnedFood)
    {
        Console.Write($"{i}. {f.Name} \n");
        i++;
    }
}

void GetInteractionOptions()
{
    Console.Write("\nOkay. Which pet do you want to interact with?\n");
    for (int i = 0; i < user.OwnedPets.Length; i++)
        Console.Write($"{i + 1}. {user.OwnedPets[i].Name}\n");
    string petAnswerStr = Console.ReadLine();
    if (Int32.TryParse(petAnswerStr, out int val) && val <= user.OwnedPets.Length && val > 0)
    {
        Console.Write("\nOkay! What do you want to do?\n1. Feed 2. Command 3. Pet 4. Play\n");
        string optStr = Console.ReadLine();
        if (Int32.TryParse(optStr, out int v))
        {
            if (v > 0 && v < 5)
            {
                if (v == 1)
                {
                    FeedPet(val);
                }
                else if (v == 2)
                {
                    CommandPet(val);
                }
                else if (v == 3)
                {
                    PetPet(val);
                }
                else if (v == 4)
                {
                    new PetConsoleService(PetService.GetAccordingPetService(user.OwnedPets[val - 1])).Play();
                }
            }
            else
            {
                throw new NoSuchOptionException();
            }
        }
        else
        {
            throw new NoSuchOptionException();
        }
    }
    else
    {
        throw new NoSuchOptionException();
    }
}
