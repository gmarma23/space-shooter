namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        protected const double baselineYRatio = 0.9;
        protected Random rand;

        public int Index { get; protected init; }
        public bool RandomMotion { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(bool randomMotion, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime, int scorePoints) :
            base(true, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime)
        {
            RandomMotion = randomMotion;
            ScorePoints = scorePoints;
            rand = new Random();
        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public void RandomUpdateDisplacement()
        {
            DisplacementX = rand.Next();
        }

        public void TargetUpdateDisplacement(Spaceship targetSpaceship)
        {

        }

        protected override void setGridLimits(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetGridMiddleY() - Height;
        }

        protected override void setBaselineY(GameGrid grid, double baselineYRatio)
        {
            baselineY = (int)(grid.DimensionX * baselineYRatio);
        }

        protected override void initializeLocationX(GameGrid grid)
        {
            int minX = grid.GetItemMinPossibleX();
            int maxX = grid.GetItemMaxPossibleX(this);
            LocationX = new Random().Next(minX, maxX);
        }

        protected override void initializeLocationY(GameGrid grid)
        {
            LocationY = grid.GetItemMinPossibleY() - Height;
        }
    }

    public enum EnemySpaceshipType 
    { 
        Fighter,
        Teleporter,
        Boss
    }
}
