using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameState
    {
        private HeroSpaceship hero;
        private List<EnemySpaceship> enemies;
        private List<int> activeEnemyIndices;
        private List<LaserBlast> activeLaserBlasts;
        private List<int> activeLaserBlastIndices;

        public int GridXDimension { get; private init; }
        public int GridYDimension { get; private init; }
        public int Score { get; private set; }

        public GameState(int gridXDimension = 1360, int gridYDimension = 760)
        {
            GridXDimension = gridXDimension;
            GridYDimension = gridYDimension;

            enemies = new List<EnemySpaceship>();
            activeLaserBlasts = new List<LaserBlast>();
        }

        public (int, int) GetHeroLocation()
        {
            return (hero.XLocation, hero.YLocation);
        }

        public (int, int) GetEnemyLocation(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            return (enemy.XLocation, enemy.YLocation);
        }

        public void FireHeroLaser()
        {
            
        }

        public void FireEnemyLaser(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            //activeLaserBlasts.Add(new LaserBlast());
        }

        public void DestroyEnemy(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            enemies.Remove(enemy);
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
