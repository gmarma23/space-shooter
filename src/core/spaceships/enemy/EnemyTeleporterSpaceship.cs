using SpaceShooter.utils;

namespace SpaceShooter.core
{
    internal class EnemyTeleporterSpaceship : EnemySpaceship, ITeleport
    {
        public EnemyTeleporterSpaceship(GameGrid grid, int hp = 400,
            int concurrentLaserBlastsCount = 1, int laserBlastDamage = 30, int laserReloadTime = 1500, int scorePoints = 2) :
            base(EnemySpaceshipType.Teleporter, hp, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
        {
            setSize(grid);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();
        }

        public override void Move()
        {
            if (moveCount == updateDisplacementFrequency)
            {
                displacementX = generateRandomDisplacement();
                displacementY = generateRandomDisplacement();
                moveCount = 0;
            }

            try
            {
                moveHorizontally();
            }
            catch (InvalidMoveException)
            {
                displacementX = generateRandomDisplacement();
            }

            try
            {
                moveVertically();
            }
            catch (InvalidMoveException)
            {
                displacementY = generateRandomDisplacement();
            }

            moveCount++;
        }

        public void Teleport()
        {
            LocationX = random.Next(minX, maxX);
            LocationY = random.Next(minY, maxY);
        }
    }
}
