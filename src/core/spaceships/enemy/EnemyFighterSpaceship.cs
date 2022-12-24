namespace SpaceShooter.core
{
    internal class EnemyFighterSpaceship : EnemySpaceship
    {
        public EnemyFighterSpaceship(GameGrid grid, bool randomMotion = true, int absMaxDisplacement = 7, int hp = 500,
            int concurrentLaserBlastsCount = 1, int laserBlastDamage = 40, int laserReloadTime = 1500, int scorePoints = 3) :
            base(randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
        {
            setSize(grid);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();
        }

        public override void Move()
        {
            int randomTargetX = generateRandomX();
            int randomTargetY = generateRandomY();
            updateDisplacement(randomTargetX, randomTargetY);
            moveHorizontally();
            moveVertically();
        }
    }
}
