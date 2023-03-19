
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    public abstract class EnemySpaceship : Spaceship
    {
        protected static Random random = new Random();
        protected const int displacementUpdateFrequency = 800;
        protected double lastDisplacementUpdateTimestamp;

        public int ScorePoints { get; protected init; }

        public EnemySpaceship(
            int hp, 
            int concurrentLaserBlastsCount, 
            int laserBlastDamage, 
            int laserReloadFrequency,
            int teleportFrequency,
            int missileCount,
            int missileDamage,
            int missileReloadFrequency,
            int scorePoints
        ) : base(
            hp, 
            concurrentLaserBlastsCount, 
            laserBlastDamage, 
            laserReloadFrequency, 
            teleportFrequency,
            missileCount, 
            missileDamage, 
            missileReloadFrequency
        )
        {
            ScorePoints = scorePoints;
            displacementY = absMaxDisplacement / 2;
            displacementX = 0;
        }

        public override void Move()
        {
            if (!IsActive)
                return;

            if (!isInsideViewport())
            {
                LocationY += DeltaTimeDisplacementY;
                return; 
            }

            updateDisplacement();
            moveHorizontally();
            moveVertically();
        }

        public override List<LaserBlast>? FireLaser(GameGrid grid)
        {
            if (!IsActive || laserIsReloading())
                return null;

            List<EnemyLaserBlast> laserBlasts = new List<EnemyLaserBlast>();

            for (int i = 0; i < ConcurrentLaserBlastsCount; i++)
                laserBlasts.Add(new EnemyLaserBlast(this, grid, i));
            lastLaserFireTimestamp = TimeManager.ElapsedGameTime;

            AudioPlayer.Instance.PlaySound(fireLaserSoundFx);
            return laserBlasts.Cast<LaserBlast>().ToList();
        }

        public override Missile? LaunchMissile(GameGrid grid)
        {
            if (!IsActive || missileCount == 0 || missileIsReloading())
                return null;

            lastMissileLaunchTimestamp = TimeManager.ElapsedGameTime;
            missileCount--;

            AudioPlayer.Instance.PlaySound(launchMissileSoundFx);
            return new EnemyMissile(this, grid);
        }

        protected override void setBounds(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetGridMiddleY() - Height;
        }

        protected override void initializeLocationX()
            => LocationX = random.Next(minX, maxX);

        protected override void initializeLocationY()
            => LocationY = minY - Height;

        protected virtual void updateDisplacement()
        {
            double constDisplacementTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastDisplacementUpdateTimestamp);

            if (constDisplacementTimeSpan < displacementUpdateFrequency)
                return;

            displacementX = generateRandomDisplacement();
            displacementY = generateRandomDisplacement();
            lastDisplacementUpdateTimestamp = TimeManager.ElapsedGameTime;
        }

        protected bool isInsideViewport()
            => LocationY >= minY;

        protected int generateRandomDisplacement(bool isAbsVal = false)
            => random.Next(isAbsVal ? 0 : -absMaxDisplacement, absMaxDisplacement);
    }
}
