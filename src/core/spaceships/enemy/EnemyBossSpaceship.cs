namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship
    {
        protected const double bossSpaceshipScaleFactor = 1.5;

        public EnemyBossSpaceship(GameGrid grid, bool randomMotion, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime, int scorePoints) :
            base(grid, randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime, scorePoints)
        {
            setSize(grid.XDimension, bossSpaceshipScaleFactor);

            YLocation = -Height;
        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
