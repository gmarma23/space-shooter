namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        public int Index { get; protected init; }
        public bool RandomMotion { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(GameGrid grid, bool randomMotion, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime, int scorePoints) :
            base(true, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime)
        {
            RandomMotion = randomMotion;
            ScorePoints = scorePoints;

            XLocation = new Random().Next(0, grid.XDimension);
            YLocation = -Height;
        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();
    }

    public enum EnemySpaceshipType 
    { 
        Fighter,
        Teleporter,
        Boss
    }
}
