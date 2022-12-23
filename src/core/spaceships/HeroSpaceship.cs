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
            initializeLocationX(grid);
            initializeLocationY(grid);
            UpdateDisplacement();
        }

        public void UpdateDisplacement()
        {
            DisplacementX = 0;
            DisplacementY = 0;

            if (GoLeft) DisplacementX -= absMaxDisplacement;
            if (GoRight) DisplacementX += absMaxDisplacement;
            if (GoUp) DisplacementY -= absMaxDisplacement;
            if (GoDown) DisplacementY += absMaxDisplacement;
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
            baselineY = (int)(grid.DimensionX * baselineYRatio) - Height;
        }

        protected override void initializeLocationX(GameGrid grid)
        {
            LocationX = grid.GetHorizontallyCenteredItemX(this);
        }

        protected override void initializeLocationY(GameGrid grid)
        {
            LocationY = baselineY;
        }
    }
}
