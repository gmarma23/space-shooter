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
            int hp = 3000,
            int concurrentLaserBlastsCount = 2, 
            int laserBlastDamage = 50, 
            int laserReloadFrequency = 1500,
            int teleportFrequency = 5000,
            int missileCount = 10, 
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
            Image = resources.Resources.img_enemy_boss_spaceship;
        }

        public override void Teleport()
        {
            if (!IsActive)
                return;

            if (teleportClockIsReloading())
                return;

            LocationX = random.Next(minX, maxX);
            LocationY = random.Next(minY, maxY);
            lastTeleportTimestamp = TimeManager.ElapsedGameTime;
            teleportSoundFx.Play();
        }

        protected override void updateDisplacement()
        {
            double constDisplacementTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastDisplacementUpdateTimestamp);

            if (constDisplacementTimeSpan < displacementUpdateFrequency)
                return;

            int deltaLocationX = target.LocationX - LocationX;
            int nexDisplacementX = Math.Sign(deltaLocationX) * generateRandomDisplacement(true);
            if (Math.Abs(deltaLocationX) >= nexDisplacementX)
                displacementX = nexDisplacementX;
            else
                displacementX = deltaLocationX;

            displacementY = generateRandomDisplacement();
            lastDisplacementUpdateTimestamp = TimeManager.ElapsedGameTime;
        }
    }
}
