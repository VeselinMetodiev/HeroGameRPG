using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RPGHeroGame
{
    public class Dungeon
    {
        private Stack<Monster> monsters;

        public int CurrentLevel;

        public Dungeon()
        {
            monsters = new Stack<Monster>();
            FillDungeon();
        }

        public Monster GetMonster()
        {
            return monsters.Peek();
        }

        public void DefeatMonster()
        {
            monsters.Pop();
            CurrentLevel++;
        }

        public bool IsEmpty()
        {
            return monsters.Count == 0;
        }

        public void FillDungeon()
        {
            int counter = 0;
            string fileName = @"C:\Users\user\source\repos\DemoOne\Dungeon.txt";
            try
            {
                // Create a StreamReader  
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    // Read line by line  
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split("-").ToArray();
                        string name = data[0];
                        int level = int.Parse(data[1]);
                        int strength = int.Parse(data[2]);
                        int agility = int.Parse(data[3]);
                        int intelligence= int.Parse(data[4]);
                        int constitution = int.Parse(data[5]);
                        int luck = int.Parse(data[6]);

                        if (counter % 3 == 0) 
                        {
                            monsters.Push(new CombatMonster(name, level, strength, agility, intelligence, constitution, luck));
                        }
                        else if(counter % 3 == 1)
                        {
                            monsters.Push(new AgileMonster(name, level, strength, agility, intelligence, constitution, luck));

                        }
                        else if(counter % 3 == 2)
                        {
                            monsters.Push(new MagicalMonster(name, level, strength, agility, intelligence, constitution, luck));
                        }

                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
