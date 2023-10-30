using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
   public class Wizard : Hero
    {
        public Wizard(string name) : base(name) { }

        public override string MainAttribute => "Intelligence";
        public override int[] GetDamage()
        {
            int minDamage = 3 * this.Strength;
            int maxDamage = 6 * this.Strength;

            return new int[] { minDamage, maxDamage };
        }

        public override int GetLife()
        {
            return (this.Constitution * 2 * (this.Level + 1));
        }
    }
}
