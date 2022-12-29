using SpaceShooter.src.core.spaceships;
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship, ILaunchMissile
    {
        protected const double bossSpaceshipScaleFactor = 2;

        protected Spaceship targetSpaceship;

        public bool MissileIsReloading { get; set; }

        public int MissileCount
        {
            get => MissileCount;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public int MissileDamage
        {
            get => MissileDamage;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public int MissileReloadTime
        {
            get => MissileReloadTime;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public EnemyBossSpaceship(GameGrid grid, Spaceship targetSpaceship, int hp = 1000,
            int concurrentLaserBlastsCount = 2, int laserBlastDamage = 50, int laserReloadTime = 1500,
            int missileCount = 3, int missileDamage = 100, int missileReloadTime = 10, int scorePoints = 5) :
            base(EnemySpaceshipType.Boss, hp, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
        {
            this.targetSpaceship = targetSpaceship;
            MissileCount = missileCount;
            MissileDamage = missileDamage;
            MissileReloadTime = missileReloadTime;

            setSize(grid, bossSpaceshipScaleFactor);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();
        }

        public override void Move()
        {
            if (moveCount == updateDisplacementFrequency)
            {
                int dx = targetSpaceship.LocationX - LocationX;
                int nexDisplacementX = Math.Sign(dx) * generateRandomDisplacement(true);
                if (Math.Abs(dx) >= nexDisplacementX)
                    displacementX = nexDisplacementX;
                else 
                    displacementX = dx;

                displacementY = generateRandomDisplacement();
                moveCount = 0;
            }

            try
            {
                moveHorizontally();
            }
            catch (InvalidMoveException) { }

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
