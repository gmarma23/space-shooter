using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameState
    {
        private GameGrid grid;
        private HeroSpaceship hero;
        private EnemySpaceship enemy;
        private List<LaserBlast> activeLaserBlasts;
        private List<int> activeLaserBlastsNumCodes;

        public int GridXDimension { get => grid.DimensionX; }
        public int GridYDimension { get => grid.DimensionY; }
        public int Score { get; private set; }

        public GameState(int gridXDimension = 1360, int gridYDimension = 760)
        {
            grid = new GameGrid(gridXDimension, gridYDimension);
            hero = new HeroSpaceship(grid);
            activeLaserBlasts = new List<LaserBlast>();
            activeLaserBlastsNumCodes = new List<int>();
        }

        public ReadOnlyCollection<int> GetActiveLaserBlastsIndices()
        {
            return activeLaserBlastsNumCodes.AsReadOnly();
        }
        
        public void RenewEnemySpaceship(EnemySpaceshipType enemyType)
        {
            enemy = enemyType switch
            {
                EnemySpaceshipType.Fighter => new EnemyFighterSpaceship(grid),
                EnemySpaceshipType.Teleporter => new EnemyTeleporterSpaceship(grid),
                EnemySpaceshipType.Boss => new EnemyBossSpaceship(grid),
                _ => throw new Exception()
            };
        }
        
        public void SpaceshipGetLocation(bool isEnemy, ref int x, ref int y)
        {
            Spaceship spaceship = getSpaceship(isEnemy);
            x = spaceship.LocationX;
            y = spaceship.LocationY;
        }

        public void SpaceshipFireLaser(bool isEnemy)
        {
            Spaceship spaceship = getSpaceship(isEnemy);
            List<LaserBlast> firedLaserBlasts = spaceship.FireLaser();
            activeLaserBlasts.AddRange(firedLaserBlasts);

            foreach(LaserBlast laserBlast in firedLaserBlasts)
            {
                int newNumCode = laserBlast.NumCode;
                Debug.Assert(!activeLaserBlastsNumCodes.Contains(newNumCode));
                activeLaserBlastsNumCodes.Add(newNumCode);
            }
        }

        public bool IsEnemyDestroyed()
        {
            return enemy.IsDestroyed;
        }

        public bool IsGameOver()
        {
            return hero.IsDestroyed;
        }

        private Spaceship getSpaceship(bool isEnemy)
        {
            return isEnemy ? enemy : hero;
        }
    }
}
