using SpaceShooter.utils;

namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship
    {
        protected const float bossSpaceshipScaleFactor = 2;

        protected IGridItem target;

        public EnemyBossSpaceship(
            GameGrid grid, 
            IGridItem target, 
            int hp = 1000,
            int concurrentLaserBlastsCount = 2, 
            int laserBlastDamage = 50, 
            int laserReloadFrequency = 1500,
            int teleportFrequency = 3000,
            int missileCount = 3, 
            int missileDamage = 100, 
            int missileReloadFrequency = 10000, 
            int scorePoints = 20
        ) : base(
            hp, 
            concurrentLaserBlastsCount, 
            laserBlastDamage,
            laserReloadFrequency, 
            teleportFrequency,
            missileCount,
            missileDamage,
            missileReloadFrequency,
            scorePoints
        )
        {
            this.target = target;
            setSize(grid, bossSpaceshipScaleFactor);
            setBounds(grid);
            initializeLocationX();
            initializeLocationY();
            Image = resources.Resources.enemy_boss_spaceship;
        }

        protected override void updateDisplacement()
        {
            int constDisplacementTimeSpan = TimeManager.GameDuration - lastDisplacementUpdateTimestamp;

            if (constDisplacementTimeSpan < displacementUpdateFrequency)
                return;

            int deltaLocationX = target.LocationX - LocationX;
            int nexDisplacementX = Math.Sign(deltaLocationX) * generateRandomDisplacement(true);
            if (Math.Abs(deltaLocationX) >= nexDisplacementX)
                displacementX = nexDisplacementX;
            else
                displacementX = deltaLocationX;

            displacementY = generateRandomDisplacement();
            lastDisplacementUpdateTimestamp = TimeManager.GameDuration;
        }
    }
}
