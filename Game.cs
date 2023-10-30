using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
	public class Game
	{

		private string name;
		private string battleClass;
		private Hero you;
		private GameController gameEngine;
		private Dungeon dungeon;

		public Game(Dungeon dungeon)
		{
			this.dungeon = dungeon;
		}
		public void Introduction()
        {
            Console.WriteLine("With the disappearance of King Targon, the country Aris fell into darkness. \n" +
                "For a quarter of a century the cities in the kingdom where conquered by beasts and monsters that keep terrorizing \n" +
                "the innocent people. More than ever Aris needs brave heroes that will kill the monsters, freed the people \n" +
                "and return the light into Aris.");
            Console.WriteLine();

            Console.WriteLine("Welcome to the world of Aris. How would you like to be called, hero?");
            name = Console.ReadLine();

            CreateHero();
            gameEngine = new GameController(you);
        }

        private void CreateHero()
        {
            do
            {
                Console.WriteLine("Now choose your battle class: (Warrior, Hunter, Wizard)");
                battleClass = Console.ReadLine();
                battleClass = battleClass.ToLower();

                if (battleClass == "warrior")
                {
                    Console.Clear();
                    Console.WriteLine($"Good choice {name} Warriors' main attribute is Strength!");
                    you = new Warrior(name);
                }
                else if (battleClass == "wizard")
                {
                    Console.Clear();
                    Console.WriteLine($"Good choice {name} Wizards' main attribute is Intelligence!");
                    you = new Wizard(name);
                }
                else if (battleClass == "hunter")
                {
                    Console.Clear();
                    Console.WriteLine($"Good choice {name} Hunters' main attribute is Agility!");
                    you = new Hunter(name);
                }
                else
                {
                    Console.WriteLine("Invalid battle class!");
                }
            } while (battleClass != "warrior" && battleClass != "hunter" && battleClass != "wizard");
        }

        public bool Win()
        {
            if (dungeon.IsEmpty())
            {
                Console.WriteLine();
               Console.WriteLine("Congratulations!, you defeated all Monsters and freed the land of Aris!");
               Console.WriteLine("The Arisian people are building a statue in your honour and they will remember you forever");
               Console.WriteLine("Your name will be added to the Hall of Fame of the best Heroes of Aris!");
				return true;
			}
			return false;

        }

		public void GameLoop()
        {
            char userChoice = ' ';
            you.CheckLevelUp();
            PrintStats();

            userChoice = Console.ReadKey().KeyChar;
            Console.Clear();
            switch (userChoice)
            {
                case '1':
                    userChoice = TrainingCamp(userChoice);
                    break;
                case '2':
                    Work();
                    break;
                case '3':
                    userChoice = Adventure();
                    break;
                case '4':
                    Dungeon();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }

        private void Dungeon()
        {
            Console.WriteLine($"You see a shadow approaching you. As it comes closer you realize it is a {dungeon.GetMonster().Name}");
            Console.WriteLine("Shakingly, you shout and run towards the monster, ready for a deadly battle!");
            Console.WriteLine("Only the gods know who will prevail.");
            Console.WriteLine();
            if (gameEngine.AttackDungeon(dungeon))
            {  //Attacking the Dungeon or top Monster of the Stack
                int reward = (dungeon.CurrentLevel + 1) * 500; // If true, the user receives gold and the Monster
                int experience = (dungeon.CurrentLevel + 1) * 25; //is popped out of the stack)
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"You defeated {dungeon.GetMonster().Name}!");
                Console.WriteLine($"Reward: {reward} Gold!");
                Console.WriteLine($"Experience gained: {experience}");
                you.IncreaseGold(reward);
                you.IncreaseExperience(experience);
                dungeon.DefeatMonster();
            }
        }

        private char TrainingCamp(char userChoice)
        {
            Console.WriteLine("You are in the training camp now. Follow the instructions:");
            while (userChoice != 'e')
            {
                Console.WriteLine("type 's' for strength, 'i' for intelligence, 'a' for agility, 'c' for constitution and 'l' for luck or 'e' to exit");
                do
                {
                    userChoice = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (userChoice == 's' || userChoice == 'a' || userChoice == 'c' || userChoice == 'l' || userChoice == 'i')
                    {
                        string attribute = "";
                        if (userChoice == 's')
                        {
                            attribute = "strength";
                        }
                        else if (userChoice == 'a')
                        {
                            attribute = "agility";
                        }
                        else if (userChoice == 'i')
                        {
                            attribute = "intelligence";
                        }
                        else if (userChoice == 'c')
                        {
                            attribute = "constitution";
                        }
                        else if (userChoice == 'l')
                        {
                            attribute = "luck";
                        }
                        Console.WriteLine(you.IncreaseAttribute(attribute));
                    }
                    else
                    {
                        if (userChoice == 'e')
                        {
                            Console.Clear(); //clear console
                            break;
                        }
                        Console.WriteLine("You entered invalid stat");
                    }
                } while (userChoice != 's' && userChoice != 'a' && userChoice != 'i' && userChoice != 'c' && userChoice != 'l');
                Console.WriteLine();
            }

            return userChoice;
        }

        private char Adventure()
        {
            char userChoice;
            Console.WriteLine("Hey, psst young hero! I have some missions for you...");
            Console.WriteLine("Choose difficulty: 1 for easy, 2 for medium, 3 for hard: ");
            userChoice = Console.ReadKey().KeyChar;
            int difficulty = userChoice - '0';
            Adventure adventure = new Adventure(difficulty, you.Level);
            if (gameEngine.GoToAdventure(adventure, difficulty))
            {
                Console.WriteLine();
                Console.WriteLine();
                int reward = adventure.Gold;
                int experience = adventure.Experience;
                Console.WriteLine("You have successfully completed your adventure!");
                Console.WriteLine($"Reward: {reward} Gold!");
                Console.WriteLine($"Experience gained: {experience}");
                you.IncreaseGold(reward);
                you.IncreaseExperience(experience);
            }
            else
            {
                Console.WriteLine("You were beaten!");
            }

            return userChoice;
        }

        private void Work()
        {
            int hours;
            Console.WriteLine("How many hours do you want to work? (1-10)");
            int.TryParse(Console.ReadLine(), out hours);
            Console.WriteLine(you.Work(hours));
            Console.WriteLine();
        }

        private void PrintStats()
        {
            Console.WriteLine();
            Console.WriteLine("Your stats:");
            Console.WriteLine($"Name: {you.Name}");
            Console.WriteLine($"Level: {you.Level}");
            Console.WriteLine($"Gold: {you.Gold}");
            Console.WriteLine($"Strength: {you.Strength}");
            Console.WriteLine($"Agility: {you.Agility}");
            Console.WriteLine($"Intelligence: {you.Intelligence}");
            Console.WriteLine($"Constitution: {you.Constitution}");
            Console.WriteLine($"Luck: {you.Luck}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Actions (type the number)");
            Console.WriteLine("1. Increase stats");
            Console.WriteLine("2. Work");
            Console.WriteLine("3. Go to adventure");
            Console.WriteLine("4. Fight in the dungeon");
        }
    }
}
