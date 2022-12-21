namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        public int Index { get; protected init; }
        public bool MovesRandomly { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int width, int height, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload,
            int missileCount, int missileDamage, int missileReload) :
            base(true, initXLocation, initYLocation, width, height, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReload,
                missileCount, missileDamage, missileReload)
        {

        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();
    }
}
