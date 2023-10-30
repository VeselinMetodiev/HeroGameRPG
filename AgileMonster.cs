using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class AgileMonster : Monster
    {

        public override string MainAttribute => "Agility";

        public AgileMonster(string name, int strength, int agility, int intelligence, int constitution, int luck, int level)
            : base(name, strength, agility, intelligence, constitution, luck, level) { }
        public override int[] GetDamage()
        {
            int minDamage = 3 * this.Strength;
            int maxDamage = 6 * this.Strength;

            return new int[] { minDamage, maxDamage };
        }

        public override int GetLife()
        {
            return (this.Constitution * 4 * (this.Level + 1));
        }
    }
}
