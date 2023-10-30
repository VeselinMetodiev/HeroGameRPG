using System;
using System.Collections.Generic;
using System.Text;

namespace RPGHeroGame
{
    public class Fighting
    {
        public bool Fight(Hero hero, Monster monster)
        {
            Random generator;
            int turn, heroLife, monsterLife, heroSkills, totalSkills, heroMinDamage, heroMaxDamage, monsterMinDamage, monsterMaxDamage, heroChanceCriticalDamage, monsterChanceCriticalDamage, heroDefence, totalDefence;
            Initialize(hero, monster, out generator, out turn, out heroLife, out monsterLife, 
                out heroSkills, out totalSkills, out heroMinDamage, out heroMaxDamage,
                out monsterMinDamage, out monsterMaxDamage, out heroChanceCriticalDamage, 
                out monsterChanceCriticalDamage, out heroDefence, out totalDefence);

            do
            {
                bool isCriticalDamage = false;
                int damage = 0;

                Console.WriteLine($"\nTurn {(turn + 1)}");

                // Let's say heroSkills = 100 and monsterSkills = 50
                // If we get a number between 0 and 99, hero attacks, and betweem 100 and 149 - monster attacks
                // The more skillfull you are, the higher the chance to attack
                int attacker = (generator.Next(0, totalSkills));

                int BlockOrNot = generator.Next(0, totalDefence);

                // Hero attacks
                if (attacker < heroSkills)
                {
                    int randomNumber = generator.Next(0, 100) + 1;
                    isCriticalDamage = IsCriticalHit(heroChanceCriticalDamage, isCriticalDamage, randomNumber);
                    damage = GetDamage(monster, generator, ref monsterLife, heroMinDamage, heroMaxDamage, heroDefence, isCriticalDamage, BlockOrNot);
                }
                // Monster attacks
                else
                {
                    isCriticalDamage = IsCriticalHit(generator, monsterChanceCriticalDamage, isCriticalDamage);
                    damage = GetDamage(hero, monster, generator, ref heroLife, monsterMinDamage, monsterMaxDamage, isCriticalDamage, BlockOrNot);
                }
                PrintRemainingLife(monster, heroLife, monsterLife);

                if (monsterLife <= 0)
                {
                    Console.WriteLine($"You defeated {monster.Name}!");
                    return true;
                }
                else if (heroLife <= 0)
                {
                    Console.WriteLine("Too weak, too slow! You were beaten!");
                    return false;
                }
                turn++;
            } while (heroLife > 0 && monsterLife > 0);
            return false;
        }

        private static void Initialize(Hero hero, Monster monster, out Random generator, out int turn, out int heroLife, out int monsterLife, out int heroSkills, out int totalSkills, out int heroMinDamage, out int heroMaxDamage, out int monsterMinDamage, out int monsterMaxDamage, out int heroChanceCriticalDamage, out int monsterChanceCriticalDamage, out int heroDefence, out int totalDefence)
        {
            generator = new Random();
            turn = 0;

            //Get Life points
            heroLife = hero.GetLife();
            monsterLife = monster.GetLife();

            // Get Skill values
            heroSkills = hero.Strength + hero.Agility + hero.Intelligence;
            int monsterSkills = monster.Strength + monster.Agility + monster.Intelligence;
            totalSkills = heroSkills + monsterSkills;

            //Get damages
            heroMinDamage = hero.GetDamage()[0];
            heroMaxDamage = hero.GetDamage()[1];
            monsterMinDamage = monster.GetDamage()[0];
            monsterMaxDamage = monster.GetDamage()[1];

            //Get the chance of critical damage
            heroChanceCriticalDamage = Math.Min(50, hero.Luck * 5 / (monster.Level * 2));
            monsterChanceCriticalDamage = Math.Min(50, monster.Luck * 5 / (hero.Level * 2));

            // Get the main attributes e.g Strength for Warrior, Intelligence for Wizard
            // Main attribute - the one used to calculate the Damage
            string heroMainAttribute = hero.MainAttribute;
            string monsterMainAttribute = monster.MainAttribute;

            // If a Warrior fights a wizard, the the wizard defends with his/her Strength points
            // And the Warrior defends with their Intelligence points
            heroDefence = 0;
            int monsterDefence = 0;

            monsterDefence = GetMonsterDefence(monster, heroMainAttribute);
            heroDefence = GetHeroDefence(monster, monsterMainAttribute);

            totalDefence = heroDefence + monsterDefence;
        }

        private static bool IsCriticalHit(int heroChanceCriticalDamage, bool isCriticalDamage, int randomNumber)
        {
            if (heroChanceCriticalDamage <= randomNumber)
            {
                isCriticalDamage = true;
                Console.WriteLine("Critical hit!");
            }

            return isCriticalDamage;
        }

        private static int GetHeroDefence(Monster monster, string monsterMainAttribute)
        {
            int heroDefence;
            if (monsterMainAttribute == "Strength")
            {
                heroDefence = monster.Strength;
            }
            else if (monsterMainAttribute == "Agility")
            {
                heroDefence = monster.Agility;
            }
            else
            {
                heroDefence = monster.Intelligence;
            }

            return heroDefence;
        }

        private static int GetMonsterDefence(Monster monster, string heroMainAttribute)
        {
            int monsterDefence;
            if (heroMainAttribute == "Strength")
            {
                monsterDefence = monster.Strength;
            }
            else if (heroMainAttribute == "Agility")
            {
                monsterDefence = monster.Agility;
            }
            else
            {
                monsterDefence = monster.Intelligence;
            }

            return monsterDefence;
        }

        private static int GetDamage(Monster monster, Random generator, ref int monsterLife, int heroMinDamage, int heroMaxDamage, int heroDefence, bool isCriticalDamage, int BlockOrNot)
        {
            int damage;
            if (BlockOrNot < heroDefence)
            {
                if (isCriticalDamage)
                {
                    damage = 2 * generator.Next(heroMinDamage, heroMaxDamage);
                }
                else
                {
                    damage = generator.Next(heroMinDamage, heroMaxDamage);
                }

                monsterLife = GetDamage(monster, monsterLife, damage);
            }
            // Monster managed to block the attack and gets twice as little damage
            else
            {
                damage = BlockDamage(monster, generator, ref monsterLife, heroMinDamage, heroMaxDamage);
            }

            return damage;
        }

        private static int GetDamage(Monster monster, int monsterLife, int damage)
        {
            Console.WriteLine("You attack");
            Console.WriteLine($"Your attack is successful. Your damage: {damage}");
            monsterLife -= damage;
            Console.WriteLine($"{monster.Name}'s remaining Life Points: {monsterLife}");
            return monsterLife;
        }

        private static int BlockDamage(Monster monster, Random generator, ref int monsterLife, int heroMinDamage, int heroMaxDamage)
        {
            int damage;
            Console.WriteLine("You attack");
            damage = generator.Next(heroMinDamage / 2, heroMaxDamage / 2);
            Console.WriteLine($"{monster.Name} partially blocked your attack! Your damage: {damage}");
            monsterLife -= damage;
            Console.WriteLine($"{monster.Name}'s remaining Life Points: {monsterLife}");
            return damage;
        }

        private static bool IsCriticalHit(Random generator, int monsterChanceCriticalDamage, bool isCriticalDamage)
        {
            int randomNumber = generator.Next(0, 100) + 1;
            if (monsterChanceCriticalDamage <= randomNumber)
            {
                isCriticalDamage = true;
                Console.WriteLine("Critical hit!");
            }

            return isCriticalDamage;
        }

        private static int GetDamage(Hero hero, Monster monster, Random generator, ref int heroLife, int monsterMinDamage, int monsterMaxDamage, bool isCriticalDamage, int BlockOrNot)
        {
            int damage;
            if (BlockOrNot > hero.Agility)
            {
                if (isCriticalDamage)
                {
                    damage = 2 * generator.Next(monsterMinDamage, monsterMaxDamage);
                }
                else
                {
                    damage = generator.Next(monsterMinDamage, monsterMaxDamage);
                }
                heroLife = Damage(hero, monster, heroLife, damage);
            }
            // You managed to block the attack and get twice as little damage
            else
            {
                damage = BlockAttack(hero, monster, generator, ref heroLife, monsterMinDamage, monsterMaxDamage);
            }

            return damage;
        }

        private static int Damage(Hero hero, Monster monster, int heroLife, int damage)
        {
            Console.WriteLine($"{monster.Name} attack");
            Console.WriteLine($"{monster.Name} attack is successful. {monster.Name} damage: {damage}");
            heroLife -= damage;
            Console.WriteLine($"{hero.Name}'s remaining Life Points: {heroLife}");
            return heroLife;
        }

        private static int BlockAttack(Hero hero, Monster monster, Random generator, ref int heroLife, int monsterMinDamage, int monsterMaxDamage)
        {
            int damage;
            Console.WriteLine($"{monster.Name} attack");
            damage = generator.Next(monsterMinDamage / 2, monsterMaxDamage / 2);
            Console.WriteLine($"You partially blocked {monster.Name} attack! {monster.Name} damage: {damage}");
            heroLife -= damage;
            Console.WriteLine($"{hero.Name}'s remaining Life Points: {heroLife}");
            return damage;
        }

        private static void PrintRemainingLife(Monster monster, int heroLife, int monsterLife)
        {
            Console.WriteLine();
            Console.WriteLine($"Your remaining life points: {heroLife}");
            Console.WriteLine($"{monster.Name} life points: {monsterLife}");
        }
    }
    }

