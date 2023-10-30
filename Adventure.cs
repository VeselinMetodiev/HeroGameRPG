using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class Adventure
    {
       Random generator = new Random();
       public int Gold { get; private set; }

       public int Experience { get; private set; }

       public Monster getRandomMonster { get; private set; }

        private string[] monsterNames = { "Wolf", "Ghost", "Wilf Bear", "Gnome", "Troll", "Shadow Demon", "Sailor", "Undead Warrior" };
       
       public Adventure(int difficulty, int heroLevel)
        {
            GenerateGold(difficulty, heroLevel);
            GenerateExperience(difficulty, heroLevel);
            getRandomMonster = GenerateMonster(difficulty, heroLevel);
        }

        private Monster GenerateMonster(int difficulty, int heroLevel)
        {
            int randomName = generator.Next(monsterNames.Length);
            string name = monsterNames[randomName];
            int monsterLevel = 1;
            //Easy
            if (difficulty == 1)
            {
                monsterLevel = Math.Max(1, heroLevel - 3);
            }
            //Medium
            else if (difficulty == 2)
            {
                monsterLevel = heroLevel;
            }
            else if (difficulty == 3)
            {
                monsterLevel = heroLevel + 1;
            }
            int min = 10, max = 10;
            if (monsterLevel <= 3)
            {
                min += monsterLevel * 2;
                max += monsterLevel * 5;
            } else if(monsterLevel >= 3 && monsterLevel < 7)
            {
                min += monsterLevel * 5;
                max += monsterLevel * 10;
            } else
            {
                min += monsterLevel * 10;
                max += monsterLevel * 20;
            }
            int strength = generator.Next(min, max);
            int agility = generator.Next(min, max);
            int intelligence = generator.Next(min, max);
            int constitution = generator.Next(min, max);
            int luck = generator.Next(min, max);

            if (strength >= agility && strength >= intelligence)
            {
                getRandomMonster = new CombatMonster(name, strength, agility, intelligence, constitution, luck, monsterLevel);
            }
            else if (agility >= strength && agility >= intelligence)
            {
                getRandomMonster = new AgileMonster(name, strength, agility, intelligence, constitution, luck, monsterLevel);
            }
            else
            {
                getRandomMonster = new MagicalMonster(name, strength, agility, intelligence, constitution, luck, monsterLevel);
            }
            return getRandomMonster;
        }
        private void GenerateExperience(int difficulty, int heroLevel)
        {
            int min = 3 * heroLevel;
            int max = 5 * heroLevel * difficulty;
            this.Experience = generator.Next(min, max);
        }

        private void GenerateGold(int difficulty, int heroLevel)
        {
            int min = 3 * heroLevel;
            int max = 5 * heroLevel * difficulty;
            this.Gold = generator.Next(min, max);
        }
    }
}
