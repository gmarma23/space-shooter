using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameState
    {
        private GameGrid grid;
        private HeroSpaceship hero;
        private List<EnemySpaceship> enemies;
        private List<int> activeEnemyIndices;
        private List<LaserBlast> activeLaserBlasts;
        private List<int> activeLaserBlastIndices;

        public int GridXDimension { get => grid.XDimension; }
        public int GridYDimension { get => grid.YDimension; }
        public int Score { get; private set; }

        public GameState(int gridXDimension = 1360, int gridYDimension = 760)
        {
            grid = new GameGrid(gridXDimension, gridYDimension);
            hero = new HeroSpaceship(grid);
            enemies = new List<EnemySpaceship>();
            activeLaserBlasts = new List<LaserBlast>();
        }
        
        public void AddEnemy(EnemySpaceshipType enemyType)
        {
            EnemySpaceship enemySpaceship;
            switch(enemyType)
            {
                case EnemySpaceshipType.Fighter:
                    enemySpaceship = new EnemyFighterSpaceship(grid);
                    break;
                case EnemySpaceshipType.Teleporter:
                    enemySpaceship = new EnemyTeleporterSpaceship(grid);
                    break;
                case EnemySpaceshipType.Boss:
                    enemySpaceship = new EnemyBossSpaceship(grid);
                    break;
                default:
                    throw new Exception();
            }
            enemies.Add(enemySpaceship);
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
            List<LaserBlast> firedHeroLaserBlasts = hero.FireLaser();
            activeLaserBlasts.AddRange(firedHeroLaserBlasts);
        }

        public void FireEnemyLaser(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            List<LaserBlast> firedEnemyLaserBlasts = enemy.FireLaser();
            activeLaserBlasts.AddRange(firedEnemyLaserBlasts);
        }

        public bool IsEnemyDestroyed(int enemyIndex)
        {
            EnemySpaceship enemy = getEnemyByIndex(enemyIndex);
            return enemy.IsDestroyed;
        }

        public void RemoveEnemy(int enemyIndex)
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
