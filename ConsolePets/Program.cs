
using CommonServices.PetServices;
using CommonServices.DBServices.AuthServices;
using Pets.Models.Pets;
using Pets.Models;
using ConsolePets.Services;
using ConsolePets.Exceptions;
using CommonServices.DBServices.UserDB;
using CommonServices;
using CommonServices.DBServices.PetDB;
using Models.AuthModels;

//string uName = string.Empty;
//while (string.IsNullOrEmpty(uName))
//{
//    Console.Write("Welcome, dear fellow! What's your name?\n");
//    uName = Console.ReadLine();
//}
//User user = new(uName);


var connectionString = "Server=localhost;User Id = postgres; Password = 123; Database=pets";

var authService = new PostgresAuthService(connectionString);

var tokenResponse = new TokenResponse(Guid.Empty, DateTime.MinValue);


while(tokenResponse.Token == Guid.Empty)
{
    Console.Write("Do you want to register or log in? Provide 1 or 2 to choose:\n");
    var answer = Console.ReadLine();

    if(!Int32.TryParse(answer, out var ansInt) || ansInt > 2 || ansInt < 1)
    {
        continue;
    }

    if(ansInt == 1)
    {
        Console.Write("Ok! Provide a username: \n");
        var inputName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputName))
        {
            continue;
        }

        Console.Write("Provide a password longer of 8+ characters: \n");
        var inputPassword = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputPassword) || inputPassword.Length < 8) 
        {
            continue;
        }

        bool registered;
        try
        {
            registered = await authService.Register(inputName, inputPassword);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            continue;
        }

        if(registered)
        {
            Console.Write("Success\n");
        }
        else
        {
            Console.Write("Registration failed\n");
        }
    }
    
    if(ansInt == 2)
    {
        Console.Write("Username:\n");
        var userName = Console.ReadLine();
        Console.WriteLine("Password:\n");
        var password = Console.ReadLine(); 

        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            continue;
        }

        try
        {
            tokenResponse = await authService.LogIn(userName, password);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            continue;
        }

        if(tokenResponse.Token == Guid.Empty)
        {
            Console.Write("Something went wrong.\n");
        }
    }
}

var userDB = await PostgresUserDBService.UserDBFactory(connectionString, tokenResponse.Token);

var user = userDB.UserToServe;

UserConsoleService userConsoleService = new(new UserService(userDB));


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
                await GetInteractionOptionsAsync();
            }
            else if (value == 2)
            {
                ViewCurrent();
            }
            else if (value == 3)
            {
                Console.Write("\n Great! Let's add another pet");
                bool petAdded = await userConsoleService.AddPetAsync();
                foreach (Pet pet in user.OwnedPets)
                {
                    Console.Write(pet.Name + "\n");
                }
            }
            else if (value == 4)
            {
                await userConsoleService.WorkAsync(connectionString);
            }
            else if (value == 5)
            {
                await userConsoleService.BuyFoodAsync();
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
async Task FeedPetAsync(int val)
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
                PetConsoleService petConsoleService = new(PetService.GetAccordingPetService(new PostgresPetDBService(connectionString, user.OwnedPets[val - 1])));
                await petConsoleService.GetFedAsync(user, user.OwnedFood[foodAnswer - 1]);
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

async Task CommandPetAsync(int val)
{
    await userConsoleService.CommandAsync(user.OwnedPets[val - 1], connectionString);
}

async Task PetPetAsync(int val)
{
    PetConsoleService petConsoleService = new(PetService.GetAccordingPetService(new PostgresPetDBService(connectionString, user.OwnedPets[val - 1])));
    await petConsoleService.GetPetAsync();
}

void ViewCurrent()
{
    Console.Write("\nCurrent Pets:\n");
    foreach (Pet p in user.OwnedPets)
    {
        Console.Write($"\n\n{p.Name} {p}\nMood: {p.Mood}\nHunger: {p.Hunger}");
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

async Task GetInteractionOptionsAsync()
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
                    await FeedPetAsync(val);
                }
                else if (v == 2)
                {
                    await CommandPetAsync(val);
                }
                else if (v == 3)
                {
                    await PetPetAsync(val);
                }
                else if (v == 4)
                {
                    await new PetConsoleService(PetService.GetAccordingPetService(new PostgresPetDBService(connectionString, user.OwnedPets[val - 1]))).PlayAsync();
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
