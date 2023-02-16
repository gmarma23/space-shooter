using System.Diagnostics;

namespace SpaceShooter.core
{
    internal class GameState
    {
        private readonly GameGrid grid;
        
        private EnemySpaceship enemy;
        private readonly HeroSpaceship hero;
        private readonly List<LaserBlast> activeLaserBlasts;

        public int GridDimensionX { get => grid.DimensionX; }
        public int GridDimensionY { get => grid.DimensionY; }
        public int Score { get; private set; }

        public GameState(int gridDimensionX = 1360, int gridDimensionY = 760)
        {
            grid = new GameGrid(gridDimensionX, gridDimensionY);
            hero = new HeroSpaceship(grid);
            activeLaserBlasts = new List<LaserBlast>();
        }
        
        public void RenewEnemySpaceship(EnemySpaceshipType enemyType)
        {
            enemy = enemyType switch
            {
                EnemySpaceshipType.Fighter => new EnemyFighterSpaceship(grid),
                EnemySpaceshipType.Teleporter => new EnemyTeleporterSpaceship(grid),
                EnemySpaceshipType.Boss => new EnemyBossSpaceship(grid, hero),
                _ => throw new Exception()
            };
        }

        public IGridItem GetSpaceshipToDraw(bool isHero) => getSpaceship(isHero);

        public IGridItem? GetLaserBlastToDraw(int numCode) => getLaserBlastByNumCode(numCode);

        public IControls GetControlableHero() => (IControls)getSpaceship(true);

        public void MoveSpaceship(bool isHero) => getSpaceship(isHero).Move();

        public void TeleportEnemySpaceship()
        {
            Debug.Assert(enemy is ITeleport); 
            ((ITeleport)enemy).Teleport();
        }

        public bool IsEnemyReadyForBattle() => enemy.IsReadyForBattle;

        public void BringEnemyToBattle() => enemy.BringToBattle();

        public List<int> SpaceshipFireLaser(bool isHero)
        {
            List<int> newlaserBlastsNumCodes = new List<int>();
            Spaceship spaceship = getSpaceship(isHero);

            List<LaserBlast> firedLaserBlasts = spaceship.FireLaser(grid);
            activeLaserBlasts.AddRange(firedLaserBlasts);

            foreach(LaserBlast laserBlast in firedLaserBlasts)
            {
                int newNumCode = laserBlast.NumCode;
                Debug.Assert(!newlaserBlastsNumCodes.Contains(newNumCode));
                newlaserBlastsNumCodes.Add(newNumCode);
            }

            return newlaserBlastsNumCodes;
        }

        public void MoveLaserBlasts()
        {
            foreach(LaserBlast laserBlast in activeLaserBlasts)
            {
                laserBlast.Move();
                checkLaserBlastHitedTargetSpaceship(laserBlast);
            }
        }

        public List<int> DisposeInactiveLaserBlast()
        {
            List<int> disposedLaserBlastsNumCodes = new List<int>();
            for (int i = activeLaserBlasts.Count - 1; i >= 0; i--)
            {
                if (isActiveLaserBlast(activeLaserBlasts[i]))
                    continue;
                disposedLaserBlastsNumCodes.Add(activeLaserBlasts[i].NumCode);
                activeLaserBlasts.RemoveAt(i);
            }
            return disposedLaserBlastsNumCodes;
        }

        public void IsHeroLaserReloading(bool isReloading)
        {
            hero.LaserIsReloading = isReloading;
        }

        public int GetSpaceshipLaserReloadTime(bool isHero)
        {
            return getSpaceship(isHero).LaserReloadTime;
        }

        public bool IsEnemyDestroyed()
        {
            return enemy.IsDestroyed;
        }

        public bool IsGameOver()
        {
            return hero.IsDestroyed;
        }

        public double GetSpaceshipAvailableHealthRatio(bool isHero)
        {
            return getSpaceship(isHero).GetAvailableHealthRatio();
        }

        public EnemySpaceshipType GetEnemySpaceshipType()
        {
            return enemy.Type;
        }

        public bool IsHeroLaserBlast(int numCode)
        {
            LaserBlast? laserBlast = getLaserBlastByNumCode(numCode);
            Debug.Assert(laserBlast != null);
            return laserBlast.IsHero;
        }

        private bool isActiveLaserBlast(LaserBlast laserBlast)
        {
            return !laserBlast.HasHitedTarget && !laserBlast.IsOutOfBounds;
        }

        private void checkLaserBlastHitedTargetSpaceship(LaserBlast laserBlast)
        {
            if (laserBlast.IsOutOfBounds || laserBlast.HasHitedTarget)
                return;

            Spaceship targetSpaceship = getSpaceship(!laserBlast.IsHero);

            if (!GameGrid.ItemsIntersect(laserBlast, targetSpaceship))
                return;

            targetSpaceship.TakeDamage(laserBlast.Damage);
            laserBlast.HasHitedTarget = true;

            if (!targetSpaceship.IsHero && targetSpaceship.IsDestroyed)
                Score += ((EnemySpaceship)targetSpaceship).ScorePoints;
        }

        private LaserBlast? getLaserBlastByNumCode(int numCode)
        {
            return activeLaserBlasts.Find(laserBlast => laserBlast.NumCode == numCode);
        }

        private Spaceship getSpaceship(bool isHero)
        {
            return isHero ? hero : enemy;
        }
    }
}
