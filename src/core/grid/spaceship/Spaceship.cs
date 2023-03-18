using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    public abstract class Spaceship : GridItem, IHPGridItem, IFireLaser, ITeleport, ILaunchMissile
    {
        protected double lastTeleportTimestamp;
        protected double lastLaserFireTimestamp;
        protected double lastMissileLaunchTimestamp;

        protected int teleportFrequency;
        protected int laserReloadFrequency;
        protected int missileReloadFrequency;
        protected int missileCount;
        protected int availableHP;

        protected static readonly AudioStreamPlayer teleportSoundFx = new(Resources.aud_teleport);
        protected static readonly AudioStreamPlayer fireLaserSoundFx = new(Resources.aud_laser_blast);
        protected static readonly AudioStreamPlayer launchMissileSoundFx = new(Resources.aud_launch_missile);

        public int TotalHP { get; protected init; }

        public int AvailableHP
        {
            get => availableHP;
            protected set
            {
                if (value < 0)
                    availableHP = 0;
                else if (value > TotalHP)
                    availableHP = TotalHP;
                else
                    availableHP = value;
            }
        }

        public int LaserBlastDamage { get; protected set; }

        public int ConcurrentLaserBlastsCount { get; protected set; }

        public int MissileDamage { get; protected set; }

        public Spaceship(
            int hp, 
            int concurrentLaserBlastsCount, 
            int laserBlastDamage, 
            int laserReloadFrequency, 
            int teleportFrequency,
            int missileCount, 
            int missileDamage,
            int missileReloadFrequency
        )
        {
            defaultWidthRatio = 0.065f;
            defaultHeightRatio = 1.04f;
            absMaxDisplacement = 240;

            TotalHP = hp;
            AvailableHP = TotalHP;
            IsActive = true;

            ConcurrentLaserBlastsCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            this.laserReloadFrequency = laserReloadFrequency;

            MissileDamage = missileDamage;
            this.missileCount = missileCount;
            this.missileReloadFrequency = missileReloadFrequency;

            this.teleportFrequency = teleportFrequency;

            lastLaserFireTimestamp = TimeManager.ElapsedGameTime;
            lastTeleportTimestamp = TimeManager.ElapsedGameTime;
            lastMissileLaunchTimestamp = TimeManager.ElapsedGameTime;
        }
        
        public void TakeDamage(int damage) 
        {
            AvailableHP -= damage;
            if(availableHP == 0) 
                IsActive = false;
        }

        public void RestoreHealth(int health) => AvailableHP += health;

        public float GetAvailableHealthRatio() => (float)AvailableHP / (float)TotalHP;

        public abstract List<LaserBlast>? FireLaser(GameGrid grid);

        public virtual Missile? LaunchMissile(GameGrid grid) => null;

        public virtual void Teleport() { }

        protected abstract void initializeLocationX();

        protected abstract void initializeLocationY();

        protected override void moveHorizontally()
        {
            int newLocationX = LocationX + DeltaTimeDisplacementX;
            if (isOutOfBoundsX(newLocationX))
            {
                displacementX *= -1;
                return;
            }
            LocationX = newLocationX;
        }

        protected override void moveVertically()
        {
            int newLocationY = LocationY + DeltaTimeDisplacementY;
            if (isOutOfBoundsY(newLocationY))
            {
                displacementY *= -1;
                return;
            }
            LocationY = newLocationY;
        }

        protected bool laserIsReloading()
        {
            double laserInactivityTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastLaserFireTimestamp);
            return laserInactivityTimeSpan < laserReloadFrequency;
        }

        protected bool teleportClockIsReloading()
        {
            double teleportClockInactivityTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastTeleportTimestamp);
            return teleportClockInactivityTimeSpan < teleportFrequency;
        }

        protected bool missileIsReloading()
        {
            double missileInactivityTimeSpan = 1000 * (TimeManager.ElapsedGameTime - lastMissileLaunchTimestamp);
            return missileInactivityTimeSpan < missileReloadFrequency;
        }
    }
}
