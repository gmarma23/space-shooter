using SpaceShooter.src.core.spaceships;

namespace SpaceShooter.core
{
    internal class EnemyBossSpaceship : EnemySpaceship, ILaunchMissile
    {
        protected const double bossSpaceshipScaleFactor = 1.5;

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

        public EnemyBossSpaceship(GameGrid grid, bool randomMotion = false, int absMaxDisplacement = 5, int hp = 1000,
            int concurrentLaserBlastsCount = 2, int laserBlastDamage = 50, int laserReloadTime = 1,
            int missileCount = 3, int missileDamage = 100, int missileReloadTime = 10, int scorePoints = 5) :
            base(randomMotion, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime, scorePoints)
        {
            setSize(grid, bossSpaceshipScaleFactor);
            setGridLimits(grid);
            initializeLocationX();
            initializeLocationY();
        }
    }
}
