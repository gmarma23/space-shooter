using SpaceShooter.src.core.grid;

namespace SpaceShooter.core
{
    public class GameState : IGameStateUI
    {
        private static readonly Random random = new Random(Environment.TickCount);

        private EnemySpaceship enemy;
        private readonly HeroSpaceship hero;
        private readonly List<CollidableItem> activeCollidableItems;
        private int waves;

        public GameGrid Grid { get; private init; }
        public int Score { get; private set; }

        public GameState(int gridDimensionX, int gridDimensionY)
        {
            Grid = new GameGrid(gridDimensionX, gridDimensionY);
            hero = new HeroSpaceship(Grid);
            activeCollidableItems = new List<CollidableItem>();
            Score = 0;
            waves = 0;
        }
        
        public void RenewEnemySpaceship()
        {
            if (enemy != null && enemy.IsActive)
                return;

            waves++;
            if (enemy != null)
            {
                Score += enemy.ScorePoints;
                releasePowerUp();
            }

            if (waves % 6 == 0)
            {
                enemy = new EnemyBossSpaceship(Grid, hero);
                return;
            }
            
            int selectedIndex = waves == 1 ? 0 : random.Next(0, 3);
            enemy = selectedIndex switch
            {
                0 => new EnemyFighterSpaceship(Grid),
                1 => new EnemyTeleporterSpaceship(Grid),
                2 => new EnemyBomberSpaceship(Grid),
                _ => new EnemyFighterSpaceship(Grid)
            };
        }

        public IHPGridItem GetSpaceshipToDraw(bool isHero) => getSpaceship(isHero);

        public IGridItem? GetCollidableItemToDraw(int id) 
            => activeCollidableItems.Single(item => item.ID == id);

        public List<int> GetActiveCollidableItemIDs() 
            => activeCollidableItems.Select(item => item.ID).ToList();

        public IControls GetControlableHero() => hero;

        public void SpaceshipFireLaser(bool isHero)
        {
            Spaceship spaceship = getSpaceship(isHero);
            List<LaserBlast>? firedLaserBlasts = spaceship.FireLaser(Grid);

            if (firedLaserBlasts == null)
                return;

            foreach (var laserBlast in firedLaserBlasts)
                laserBlast.Target = getSpaceship(!isHero);

            activeCollidableItems.AddRange(firedLaserBlasts);
        }

        public void MoveGridItems()
        {
            hero.Move(); 
            enemy.Move();
            foreach (var item in activeCollidableItems)
                item.Move();
        }

        public void EnemyTeleport() => enemy.Teleport();

        public void EnemyLaunchMissile()
        {
            Missile? launchedMissile = enemy.LaunchMissile(Grid);

            if (launchedMissile == null)
                return;

            launchedMissile.Target = hero;

            activeCollidableItems.Add(launchedMissile);
        }

        public bool IsEnemyDestroyed() => !enemy.IsActive;

        public bool IsGameOver() => !hero.IsActive;

        public void DisposeInactiveCollidableItems()
        {
            for (int i = activeCollidableItems.Count - 1; i >= 0; i--)
                if (!activeCollidableItems[i].IsActive)
                    activeCollidableItems.RemoveAt(i);
        }

        private void releasePowerUp()
        {
            if (random.Next(0, 4) != 0)
                return;

            var healthKit = new HealthKit(Grid, enemy)
            {
                Target = hero
            };
            activeCollidableItems.Add(healthKit);
        }
        
        private Spaceship getSpaceship(bool isHero) => isHero ? hero : enemy;
    }
}
