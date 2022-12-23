namespace SpaceShooter.core
{
    internal class HeroSpaceship : Spaceship
    {
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }

        public HeroSpaceship(GameGrid grid, int absMaxDisplacement, int hp, 
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime) :
            base (false, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, 
                missileCount, missileDamage, missileReloadTime)
        {
            initializeDirectionBooleans();

            setSize(grid);
            setInitXLocation(grid);
            setInitYLocation(grid);
            UpdateDisplacement();
        }

        public void UpdateDisplacement()
        {
            XDisplacement = 0;
            YDisplacement = 0;

            if (GoLeft) XDisplacement -= absMaxDisplacement;
            if (GoRight) XDisplacement += absMaxDisplacement;
            if (GoUp) YDisplacement -= absMaxDisplacement;
            if (GoDown) YDisplacement += absMaxDisplacement;
        }

        protected void initializeDirectionBooleans()
        {
            GoLeft = false;
            GoRight = false;
            GoUp = false;
            GoDown = false;
        }

        protected override void setInitXLocation(GameGrid grid)
        {
            throw new NotImplementedException();
        }

        protected override void setInitYLocation(GameGrid grid)
        {
            throw new NotImplementedException();
        }
    }
}
