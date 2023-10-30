using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class CombatMonster : Monster
    {

        public override string MainAttribute => "Strength";

        public CombatMonster(string name, int strength, int agility, int intelligence, int constitution, int luck, int level)
            : base(name, strength, agility, intelligence, constitution, luck, level) {}
        public override int[] GetDamage()
        {
            int minDamage = 2 * this.Strength;
            int maxDamage = 4 * this.Strength;

            return new int[] {minDamage, maxDamage };
        }

        public override int GetLife()
        {
            return (this.Constitution * 6 * (this.Level + 1));
        }
    }
}
