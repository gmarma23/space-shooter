namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        public int Index { get; protected init; }
        public bool MovesRandomly { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(
            int hp, int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload, 
            int missileCount, int missileDamage, int missileReload, bool movesRandomly, int scorePoints) : 
            base(hp, concurrentLaserBlastsCount, laserBlastDamage, laserReload, missileCount, missileDamage, missileReload)
        {
            MovesRandomly = movesRandomly;
            ScorePoints = scorePoints;
        }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();
    }
}
