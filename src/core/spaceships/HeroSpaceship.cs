namespace SpaceShooter.core
{
    internal class HeroSpaceship : Spaceship
    {
        protected const double baselineYRatio = 0.1;

        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }

        public HeroSpaceship(GameGrid grid, int absMaxDisplacement = 7, int hp = 700, 
            int concurrentLaserBlastsCount = 2, int laserBlastDamage = 40, int laserReloadTime = 1,
            int missileCount = 0, int missileDamage = 0, int missileReloadTime = 0) :
            base (false, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, 
                missileCount, missileDamage, missileReloadTime)
        {
            initializeDirectionBooleans();

            setSize(grid);
            setBaselineY(grid, baselineYRatio);
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

        protected override void setBaselineY(GameGrid grid, double baselineYRatio)
        {
            BaselineY = (int)(grid.XDimension * baselineYRatio) - Height;
        }

        protected override void setInitXLocation(GameGrid grid)
        {
            XLocation = grid.GetHorizontallyCenteredItemX(this);
        }

        protected override void setInitYLocation(GameGrid grid)
        {
            YLocation = BaselineY;
        }
    }
}
