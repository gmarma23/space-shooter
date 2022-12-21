namespace SpaceShooter.core
{
    internal class EnemyFighterSpaceship : EnemySpaceship
    {
        public EnemyFighterSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int width, int height, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload,
            int missileCount, int missileDamage, int missileReload) :
            base(defaultDisplacement, initXLocation, initYLocation, width, height, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReload,
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
