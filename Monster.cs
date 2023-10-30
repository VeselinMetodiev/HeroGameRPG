using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public abstract class Monster : IEntity
    {
        public string Name { get; }
        public int Strength { get; }

        public int Agility { get; }

        public int Intelligence { get; }

        public int Constitution { get; }

        public int Luck { get; }

        public int Level { get; private set; }

        public abstract string MainAttribute { get; }

        public Monster(string name, int strength, int agility, int intelligence, int constitution, int luck, int level)
        {
            this.Name = name;
            this.Strength = strength;
            this.Agility = agility;
            this.Intelligence = intelligence;
            this.Constitution = constitution;
            this.Luck = luck;
            this.Level = level;
        }

        public abstract int[] GetDamage();

        public abstract int GetLife();

        public override string ToString()
        {
            return $" A Monster named {Name} of type {this.GetType().Name} with Strength: {Strength}, Agility {Agility}, Intelligence: {Intelligence}, Constitution: {Constitution}, Luck: {Luck}";
        }
    }
}
