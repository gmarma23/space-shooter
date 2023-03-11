using SpaceShooter.src.core.grid;

namespace SpaceShooter.core
{
    public class GameState : IGameStateUI
    {
        private EnemySpaceship enemy;
        private readonly HeroSpaceship hero;
        private readonly List<Weapon> activeWeapons;

        public GameGrid Grid { get; private init; }
        public int Score { get; private set; }

        public GameState(int gridDimensionX = 1360, int gridDimensionY = 760)
        {
            Grid = new GameGrid(gridDimensionX, gridDimensionY);
            hero = new HeroSpaceship(Grid);
            activeWeapons = new List<Weapon>();
        }
        
        public void RenewEnemySpaceship()
        {
            if (enemy != null && enemy.IsActive)
                return;

            enemy = new EnemyBomberSpaceship(Grid);
        }

        public IHPGridItem GetSpaceshipToDraw(bool isHero) => getSpaceship(isHero);

        public IGridItem? GetWeaponToDraw(int id) 
            => activeWeapons.Single(weapon => weapon.ID == id);

        public List<int> GetActiveWeaponIDs() 
            => activeWeapons.Select(weapon => weapon.ID).ToList();

        public IControls GetControlableHero() => (IControls)getSpaceship(true);

        public void SpaceshipFireLaser(bool isHero)
        {
            Spaceship spaceship = getSpaceship(isHero);
            List<LaserBlast>? firedLaserBlasts = spaceship.FireLaser(Grid);

            if (firedLaserBlasts == null)
                return;

            foreach (var laserBlast in firedLaserBlasts)
                laserBlast.Target = getSpaceship(!isHero);

            activeWeapons.AddRange(firedLaserBlasts);
        }

        public void MoveGridItems()
        {
            hero.Move(); 
            enemy.Move();
            foreach (var weapon in activeWeapons)
                weapon.Move();
        }

        public void EnemyTeleport() => enemy.Teleport();

        public void EnemyLaunchMissile()
        {
            Missile? launchedMissile = enemy.LaunchMissile(Grid);

            if (launchedMissile == null)
                return;

            launchedMissile.Target = hero;

            activeWeapons.Add(launchedMissile);
        }

        public bool IsEnemyDestroyed() => !enemy.IsActive;

        public bool IsGameOver() => !hero.IsActive;

        public void DisposeInactiveWeapons()
        {
            for (int i = activeWeapons.Count - 1; i >= 0; i--)
                if (!activeWeapons[i].IsActive)
                    activeWeapons.RemoveAt(i);
        }
        
        private Spaceship getSpaceship(bool isHero) => isHero ? hero : enemy;
    }
}
