using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class Warrior : Hero
    {

        public Warrior(string name) : base(name) { }

        public override string MainAttribute => "Strength";
        public override int[] GetDamage()
        {
            int minDamage = 2 * this.Strength;
            int maxDamage = 4 * this.Strength;

            return new int[] { minDamage, maxDamage };
        }

        public override int GetLife()
        {
            return (this.Constitution * 6 * (this.Level + 1));
        }
    }
}
