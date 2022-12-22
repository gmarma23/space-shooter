namespace SpaceShooter.core
{
    internal class EnemyTeleporterSpaceship : EnemySpaceship
    {
        public EnemyTeleporterSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int gridXDimension, int hp,
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
                         int missileCount, int missileDamage, int missileReload) :
            base(defaultDisplacement, initXLocation, initYLocation, gridXDimension, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReload)
        {

        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
