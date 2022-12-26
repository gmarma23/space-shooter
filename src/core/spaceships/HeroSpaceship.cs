using SpaceShooter.utils;

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
            int concurrentLaserBlastsCount = 2, int laserBlastDamage = 40, int laserReloadTime = 1) :
            base (true, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime)
        {
            AbsMaxDisplacement = absMaxDisplacement;
            initializeDirectionBooleans();

            setSize(grid);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();
        }

        public override void Move()
        {
            updateDisplacement();

            try
            {
                moveHorizontally();
            }
            catch (InvalidMoveException) { }

            try
            {
                moveVertically();
            }
            catch (InvalidMoveException) { }
        }

        protected void updateDisplacement()
        {
            displacementX = 0;
            displacementY = 0;

            if (GoLeft) displacementX -= absMaxDisplacement;
            if (GoRight) displacementX += absMaxDisplacement;
            if (GoUp) displacementY -= absMaxDisplacement;
            if (GoDown) displacementY += absMaxDisplacement;
        }

        protected void initializeDirectionBooleans()
        {
            GoLeft = false;
            GoRight = false;
            GoUp = false;
            GoDown = false;
        }

        protected override void setGridLimits(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetGridMiddleY();
            maxY = grid.GetItemMaxPossibleY(this);
        }

        protected override void initializeLocationX()
        {
            LocationX = (minX + maxX) / 2;
        }

        protected override void initializeLocationY()
        {
            LocationY = (minY + maxY) / 2;
        }
    }
}
