namespace SpaceShooter.core
{
    internal class EnemyTeleporterSpaceship : EnemySpaceship
    {
        public EnemyTeleporterSpaceship(GameGrid grid, bool randomMotion = true, int absMaxDisplacement = 7, int hp = 400,
            int concurrentLaserBlastsCount = 1, int laserBlastDamage = 30, int laserReloadTime = 1500,
            int missileCount = 0, int missileDamage = 0, int missileReloadTime = 0, int scorePoints = 2) :
            base(randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime, scorePoints)
        {
            setSize(grid);
            setBaselineY(grid, baselineYRatio);
            setInitXLocation(grid);
            setInitYLocation(grid);
        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
