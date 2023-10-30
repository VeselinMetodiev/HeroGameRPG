using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class GameController : IPlayer
    {
        private Hero hero;
        private Fighting fighting = new Fighting();
        public GameController(Hero hero)
        {
            this.hero = hero;
        }
        public bool AttackDungeon(Dungeon dungeon)
        {
            Monster currentMonster = dungeon.GetMonster();
            return fighting.Fight(hero, currentMonster);
        }

        public bool GoToAdventure(Adventure adventure, int difficulty)
        {
            Monster getMonster = adventure.getRandomMonster;
            return fighting.Fight(hero, getMonster);
        }

        public void GoToWork(int hours)
        {
            hero.Work(hours);
        }

        public void IncreaseAttribute(string attribute)
        {
            hero.IncreaseAttribute(attribute);
        }
    }
}
