namespace SpaceShooter.core
{
    internal class EnemyTeleporterSpaceship : EnemySpaceship
    {
        public EnemyTeleporterSpaceship(GameGrid grid, bool randomMotion, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime, int scorePoints) :
            base(grid, randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime, scorePoints)
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
