using SpaceShooter.src.core.spaceships;

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

        public EnemyBossSpaceship(GameGrid grid, Spaceship targetSpaceship, int absMaxDisplacement = 5, int hp = 1000,
            int concurrentLaserBlastsCount = 2, int laserBlastDamage = 50, int laserReloadTime = 1500,
            int missileCount = 3, int missileDamage = 100, int missileReloadTime = 10, int scorePoints = 5) :
            base(EnemySpaceshipType.Boss, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, scorePoints)
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
            int targetX = targetSpaceship.LocationX;
            int randomTargetY = generateRandomY();
            updateDisplacement(targetX, randomTargetY);
            moveHorizontally();
            moveVertically();
        }
    }
}
