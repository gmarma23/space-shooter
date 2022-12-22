namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        public int Index { get; protected init; }
        public bool MovesRandomly { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int gridXDimension, int hp,
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
                         int missileCount, int missileDamage, int missileReload) :
            base(true, initXLocation, initYLocation, gridXDimension, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReload)
        {

        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();
    }
}
