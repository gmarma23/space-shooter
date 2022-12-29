using SpaceShooter.utils;

namespace SpaceShooter.core
{
    internal class EnemyFighterSpaceship : EnemySpaceship
    {
        public EnemyFighterSpaceship(GameGrid grid, int hp = 500,
            int concurrentLaserBlastsCount = 1, int laserBlastDamage = 40, int laserReloadTime = 1500, int scorePoints = 3) :
            base(EnemySpaceshipType.Fighter, hp, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
        {
            setSize(grid);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();

            displacementX = generateRandomDisplacement();
            displacementY = generateRandomDisplacement();
        }

        public override void Move()
        {
            if(moveCount == updateDisplacementFrequency)
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
    }
}
