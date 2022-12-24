namespace SpaceShooter.core
{
    internal class EnemyTeleporterSpaceship : EnemySpaceship, ITeleport
    {
        protected Random random;

        public EnemyTeleporterSpaceship(GameGrid grid, int absMaxDisplacement = 7, int hp = 400,
            int concurrentLaserBlastsCount = 1, int laserBlastDamage = 30, int laserReloadTime = 1500, int scorePoints = 2) :
            base(hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
        {
            random = new Random();
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

        public void Teleport()
        {
            LocationX = generateRandomX();
            LocationY = generateRandomY();
        }
    }
}
