namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship
    {
        protected const double bossSpaceshipWidthRatio = 1.5 * defaultWidthRatio;

        public EnemyBossSpaceship(GameGrid grid, bool randomMotion, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
            int missileCount, int missileDamage, int missileReloadTime, int scorePoints) :
            base(grid, randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime, scorePoints)
        {
            Width = (int)(Width * bossSpaceshipWidthRatio);
            Height = (int)(Width * defaultHeightRatio);

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
