using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameState
    {
        private const double normalSpaceshipWidthRatio = 0.05;
        private const double bossSpaceshipWidthRatio = 1.5;
        private const double spaceshipHeightRatio = 1.05;
        private const double laserBlastWidthRatio = 0.05;
        private const double laserBlastHeightRatio = 7;

        private HeroSpaceship hero;
        private List<EnemySpaceship> enemies;
        private List<int> activeEnemyIndices;
        private List<LaserBlast> activeLaserBlasts;
        private List<int> activeLaserBlastIndices;

        private readonly int normalSpaceshipWidth;
        private readonly int normalSpaceshipHeight;
        private readonly int bossSpaceshipWidth;
        private readonly int bossSpaceshipHeight;
        private readonly int normalLaserBlastWidth;
        private readonly int bossLaserBlastWidth;
        private readonly int normalLaserBlastHeight;
        private readonly int bossLaserBlastHeight;
        private readonly int laserBlastDisplacement;

        public int GridXDimension { get; private init; }
        public int GridYDimension { get; private init; }
        public int Score { get; private set; }

        public GameState(int gridXDimension = 1360, int gridYDimension = 760)
        {
            GridXDimension = gridXDimension;
            GridYDimension = gridYDimension;

            enemies = new List<EnemySpaceship>();
            activeLaserBlasts = new List<LaserBlast>();

            normalSpaceshipWidth = (int)(GridXDimension * normalSpaceshipWidthRatio);
            normalSpaceshipHeight = (int)(normalSpaceshipWidth * spaceshipHeightRatio);
            bossSpaceshipWidth = (int)(normalSpaceshipWidth * bossSpaceshipWidthRatio);
            bossSpaceshipHeight = (int)(bossSpaceshipWidth * spaceshipHeightRatio);
            normalLaserBlastWidth = (int)(normalSpaceshipWidth * laserBlastWidthRatio);
            normalLaserBlastHeight = (int)(normalLaserBlastWidth * laserBlastHeightRatio);
            bossLaserBlastWidth = (int)(bossSpaceshipWidth * bossSpaceshipWidthRatio);
            bossLaserBlastHeight = (int)(bossLaserBlastWidth * laserBlastHeightRatio);
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
            int laserBlastXLocation, laserBlastYLocation;
            for (int i = 0; i < hero.ConcurrentLaserBlastsCount; i++)
            {
                laserBlastXLocation = hero.XLocation + (i * hero.Width / (hero.ConcurrentLaserBlastsCount + 1)) - (normalLaserBlastWidth / 2);
                laserBlastYLocation = hero.YLocation - normalLaserBlastHeight;
                activeLaserBlasts.Add(new LaserBlast(false, hero.LaserBlastDamage, laserBlastDisplacement, laserBlastXLocation, laserBlastYLocation, normalLaserBlastWidth, normalLaserBlastHeight));
            }
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
