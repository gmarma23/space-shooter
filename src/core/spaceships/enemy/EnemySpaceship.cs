namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        protected const double baselineYRatio = 0.9;

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
        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();

        protected override void setBaselineY(GameGrid grid, double baselineYRatio)
        {
            BaselineY = (int)(grid.XDimension * baselineYRatio);
        }

        protected override void setInitXLocation(GameGrid grid)
        {
            int minX = grid.GetItemMinPossibleX();
            int maxX = grid.GetItemMaxPossibleX(this);
            XLocation = new Random().Next(minX, maxX);
        }

        protected override void setInitYLocation(GameGrid grid)
        {
            YLocation = grid.GetItemMinPossibleY() - Height;
        }
    }

    public enum EnemySpaceshipType 
    { 
        Fighter,
        Teleporter,
        Boss
    }
}
