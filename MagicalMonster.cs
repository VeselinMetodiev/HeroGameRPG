using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class MagicalMonster : Monster
    {

        public override string MainAttribute => "Intelligence";

        public MagicalMonster(string name, int strength, int agility, int intelligence, int constitution, int luck, int level)
            : base(name, strength, agility, intelligence, constitution, luck, level) { }
        public override int[] GetDamage()
        {
            int minDamage = 4 * this.Strength;
            int maxDamage = 8 * this.Strength;

            return new int[] { minDamage, maxDamage };
        }

        public override int GetLife()
        {
            return (this.Constitution * 2 * (this.Level + 1));
        }
    }
}
