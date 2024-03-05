using Pets.Models;
using Pets.Models.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePets.Services;
using CommonServices;
using CommonServices.PetServices;

using Pets.Models.Enumerations;
using ConsolePets.Exceptions;

namespace ConsolePets.Services
{
    internal class UserConsoleService
    {
        internal UserService _userService;
        
        internal UserConsoleService(UserService userService) 
        {
            _userService = userService;
        }

        public void Work()
        {
            _userService.Work();
            StringBuilder text = new StringBuilder("\nGotta work to feed my pets... See you!");
            text.Append($"\nIt ain't much but it's fair work. I can finally rest. \nBalance = {_userService.UserToServe.CashBalance}");
            Console.Write(text.ToString());
        }

        public void Command(Pet pet)
        {
            PetService petService = PetService.GetAccordingPetService(pet);
            string[] commands = petService.GetCommands();
            Console.Write("\nWhat command do you want to execute? Provide number: \n");
            StringBuilder commandList = new("");
            int i = 1;
            foreach (var command in commands)
            {
                commandList.Append($"\n{i}. " + command);
                i++;
            }
            Console.Write(commandList + "\n");
            string answer = Console.ReadLine();
            if (string.IsNullOrEmpty(answer) || !int.TryParse(answer, out int userAnswer))
            {
                throw new NoSuchOptionException();
            }
            else
            {
                if ((userAnswer > commands.Length || userAnswer < 1))
                {
                    throw new NoSuchOptionException();
                }
                else
                {
                    StringBuilder executionText = new(petService.GetCommandExecution()[userAnswer - 1]);
                    Console.Write(executionText + "\n");
                    PetFood foodBroughtByBear = _userService.Command(petService, userAnswer);
                    if(foodBroughtByBear != null)
                    {
                        StringBuilder text = new($"{pet.Name} brought {foodBroughtByBear.Name}, yay!");
                        text.Append("\nNow you have:");
                        foreach (PetFood p in _userService.UserToServe.OwnedFood)
                        {
                            text.Append("\n" + p.Name);
                        }
                        Console.Write(text.ToString());
                    }
                    Console.Write($"\n{pet.Name}'s mood is now {pet.Mood}");
                }
            }
        }

        public void BuyFood()
        {
            Console.Write("\nWhat food do you want to buy? Provide a number:\n1. CatFood,\n2. DogFood,\n3. BearFood,\n4. ParrotFood\n");
            string answer = Console.ReadLine();
            int answerInt = 0;
            if ((string.IsNullOrEmpty(answer) || !int.TryParse(answer, out answerInt)) || (answerInt <= 0 || answerInt >= 5))
            {
                throw new NoSuchOptionException();
            }
            else
            {
                FoodType petFoodType = (FoodType)answerInt;
                if (petFoodType != 0)
                {
                    _userService.BuyFood(petFoodType);
                    StringBuilder text = new StringBuilder("\nPhew... That was a long ride on my Prius. Maybe I should change my car. \nAnyway, here's what we got:\nFood:");
                    foreach (PetFood p in _userService.UserToServe.OwnedFood)
                    {
                        text.Append("\n" + p.Name);
                    }
                    text.Append($"\nBalance: {_userService.UserToServe.CashBalance}");
                    Console.Write(text.ToString());
                }
            }
        }

        public bool AddPet()
        {
            Console.Write("\nWhat animal do you want to pet? Input the number of the chosen option.\n1. Bear \n __         __\r\n/  \\.-\"\"\"-./  \\\r\n\\    -   -    /\r\n |   o   o   |\r\n \\  .-'''-.  /\r\n  '-\\__Y__/-'\r\n     `---`\n2. Cat\n /\\_/\\\r\n( o.o )\r\n > ^ <\n3. Dog\n   / \\__\r\n  (    @\\___\r\n  /         O\r\n /   (_____/\r\n/_____/ \n4. Parrot\n   \\\\\r\n   (o>\r\n\\\\_//)\r\n \\_/_)\r\n  _|_\n");
            string answer = Console.ReadLine();
            if (string.IsNullOrEmpty(answer) || !int.TryParse(answer, out int answerInt) || answerInt > 4 || answerInt < 1)
            {
                throw new NoSuchOptionException();
            }
            else
            {
                Console.Write("\nOkay, will find one. How do you want to name it?\n");
                string petName = Console.ReadLine();
                if (string.IsNullOrEmpty(petName) || int.TryParse(petName, out _))
                {
                        throw new WrongNameException();
                }
                else
                {
                    Console.Write("\nHmm... That's an intersting name. It's your pet, so I'm okay with that.\nLet's choose the color you want for your pet's fur. Provide a number:\n1. Black\n2. Brown\n3. White\n4. Gray\n5. Orange\n");
                    string furString = Console.ReadLine();
                    if (string.IsNullOrEmpty(furString) || !int.TryParse(furString, out _))
                    {
                        throw new NoSuchOptionException();
                    }
                    else
                    {
                        int furAnswer = int.Parse(furString);
                        FurType furColor = (FurType)furAnswer;
                        Pet pet = null;
                        switch (answerInt)
                        {
                            case 1:
                                pet = new Bear(petName, furColor);
                                break;
                            case 2:
                                pet = new Cat(petName, furColor);
                                break;
                            case 3:
                                pet = new Dog(petName, furColor);
                                break;
                            case 4:
                                pet = new Parrot(petName, furColor);
                                break;
                            default:
                                _ = new Pet();
                                break;
                        }
                        return _userService.AddPet(pet);
                    }
                }
            }
        }
    }
}
