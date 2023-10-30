
namespace RPGHeroGame
{
    public interface IEntity
    {

        public string Name { get; }

        public int Level { get; }

        public int Strength { get; }
        public int Agility { get; }
        public int Intelligence { get; }
        public int Constitution { get; } //Stamina
        public int Luck { get; } // The higher -> more chance of critical damage

        public string MainAttribute { get; }

       
        public int GetLife();

        public int[] GetDamage(); // array with 2 values - minimum and maximum damage

    }
}
