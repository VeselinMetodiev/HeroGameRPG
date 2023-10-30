using System;

namespace RPGHeroGame
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Dungeon dungeon = new Dungeon();
            Game game = new Game(dungeon);
            game.Introduction();
            //GameEngine gameEngine = new GameEngine(hero);
            do
            {
                game.GameLoop();
            } while (!game.Win());

        }
    }
}
