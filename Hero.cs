using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public abstract class Hero : IEntity
    {
        public string Name { get; private set; }

        public int Level { get; private set; }

        public int Gold { get; private set; }

        public int Strength { get; private set; }

        public int Agility { get; private set; }

        public int Intelligence { get; private set; }

        public int Constitution { get; private set; }

        public int Luck { get; private set; }

        public int Experience { get; private set; }
        
        public abstract string MainAttribute { get; }

        public Hero(string name)
        {
            this.Name = name;
            this.Strength = 10;
            this.Agility = 10;
            this.Intelligence = 10;
            this.Constitution = 10;
            this.Luck = 10;
            this.Gold = 100;
            this.Level = 1;
            this.Experience = 0;
        }

        // Different for different types of heroes
        public abstract int[] GetDamage();
        public abstract int GetLife();

        /// Increase Stats

        private void AddStrength() => this.Strength++;
        private void AddAgility() => this.Agility++;
        private void AddIntelligence() => this.Intelligence++;
        private void AddConstitution() => this.Constitution++;
        private void AddLuck() => this.Luck++;

        /// 

        public void IncreaseGold(int value) 
        {
            this.Gold += value;
        }

        public void IncreaseExperience(int value)
        {
            this.Experience += value;
        }
        public void CheckLevelUp()
        {
            if (Experience > (30 * Level))
            {
                this.Level++;
                this.Experience -= (30 * Level);
            }
        }

        public string Work(int hours)
        {
            int salary = hours * (this.Level * 10);
            this.Gold += salary;
            return $"You've worked for {hours} hours, and earned {salary} Gold.";
        }

        public string IncreaseAttribute(string attribute)
        {
            int strTimes = this.Strength-8; // variables to hold how many times an attribute has been increased
            int agiTimes = this.Agility - 8;
            int intTimes = this.Intelligence - 8;
            int consTimes = this.Constitution - 8;
            int luckTimes = this.Luck - 8;
            if (attribute == "strength")
            {
                if (this.Gold > 5 * strTimes)
                {
                    AddStrength();
                    int price = 5 * strTimes;
                    this.Gold -= price;
                    return $"You increased Strength for {price} Gold. New Strength: {this.Strength}";
                }
                else
                {
                    return $"Not enough gold.";
                }
            }
            else if (attribute == "agility")
            {
                if (this.Gold > 5 * agiTimes)
                {
                    AddAgility();
                    int price = 5 * agiTimes;
                    this.Gold -= price;
                    return $"You increased Agility for {price} Gold. New Agility: {this.Agility}";
                }
                else
                {
                    return "Not enough gold.";
                }
            }
            else if (attribute == "intelligence")
            {
                if (this.Gold > 5 * intTimes)
                {
                    AddIntelligence();
                    int price = 5 * intTimes;
                    this.Gold -= price;
                    return $"You increased Intelligence for {price} Gold. New Intelligence: {this.Intelligence}";
                }
                else
                {
                    return "Not enough gold.";
                }
            }
            else if (attribute == "constitution")
            {
                if (this.Gold > 5 * consTimes)
                {
                    AddConstitution();
                    int price = 5 * consTimes;
                    this.Gold -= price;
                    return $"You increased Constitution for {price} Gold. New Constitution: {this.Constitution}";
                }
                else
                {
                    Console.WriteLine("Not enough gold.");
                }
            }
            else if (attribute == "luck")
            {
                if (this.Gold > 5 * luckTimes)
                {
                    AddLuck();
                    int price = 5 * luckTimes;
                    this.Gold -= price;
                    return $"You increased Luck for {price} Gold. New Luck: {this.Luck}";
                }
                else
                {
                    return "Not enough gold.";
                }
            }
            return "Invalid input";
        }
    }
}
