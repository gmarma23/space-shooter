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
            int missileReloadFrequency = 12000, 
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

        protected override void updateDisplacement()
        {
            double constDisplacementTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastDisplacementUpdateTimestamp);

            if (constDisplacementTimeSpan < displacementUpdateFrequency)
                return;

            if (target == null)
            {
                displacementX = 0;
                return;
            }

            int deltaMiddleX = target.LocationX + (target.Width / 2) - (LocationX + (Width / 2));

            if (deltaMiddleX == 0)
            {
                displacementX = 0;
                return;
            }

            int deltaMiddleXSign = Math.Sign(deltaMiddleX);
            int nexDisplacementX = deltaMiddleXSign * absMaxDisplacement;
            int newDeltaMiddleX = deltaMiddleX + nexDisplacementX;

            displacementX = deltaMiddleXSign == Math.Sign(newDeltaMiddleX) ? nexDisplacementX : deltaMiddleX;

            displacementY = generateRandomDisplacement();
            lastDisplacementUpdateTimestamp = TimeManager.ElapsedGameTime;
        }
    }
}
