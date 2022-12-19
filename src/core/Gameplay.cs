using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class Gameplay
    {
        HeroSpaceship hero;
        List<EnemySpaceship> enemies;
        List<LaserBlast> activeLaserBlasts;

        public Gameplay()
        {
            hero = new HeroSpaceship();
            enemies = new List<EnemySpaceship>();
            activeLaserBlasts = new List<LaserBlast>();
        }

        public (int, int) GetHeroDisplacement()
        {
            return (hero.XDisplacement, hero.YDisplacement);
        }

        public (int, int) GetHeroVelocity()
        {
            return (hero.XVelocity, hero.YVelocity);
        }

        public void FireHeroLaser()
        {
            activeLaserBlasts.Add(new LaserBlast());
        }

        public (int, int) GetEnemyDisplacement(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            return (enemy.XDisplacement, enemy.YDisplacement);
        }

        public (int, int) GetEnemyVelocity(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            return (enemy.XVelocity, enemy.YVelocity);
        }

        public void FireEnemyLaser(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            activeLaserBlasts.Add(new LaserBlast());
        }

        public bool IsGameOver()
        {
            return hero.IsDestroyed;
        }

        private EnemySpaceship getEnemyByIndex(int enemyIndex)
        {
            EnemySpaceship? enemy = enemies.Find(enemy => enemy.Index == enemyIndex);
            if (enemy == null)
                throw new Exception();
            else
                return enemy;
        }
    }
}
