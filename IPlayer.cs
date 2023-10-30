using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public interface IPlayer
    {
        public void GoToWork(int hours);

        public bool AttackDungeon(Dungeon dungeon);

        public bool GoToAdventure(Adventure adventure, int difficulty);

        public void IncreaseAttribute(string attribute);
    }
}
