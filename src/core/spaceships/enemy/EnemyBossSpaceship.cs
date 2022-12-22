namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship
    {
        protected const double bossSpaceshipWidthRatio = 1.5;

        public EnemyBossSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int gridXDimension, int hp,
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
                         int missileCount, int missileDamage, int missileReloadTime) :
            base(defaultDisplacement, initXLocation, initYLocation, gridXDimension, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime)
        {
            Width = (int)(Width * bossSpaceshipWidthRatio);
            Height = (int)(Width * heightRatio);
        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
