namespace SpaceShooter.core
{
    public abstract class Spaceship : GridItem, IHPGridItem, IFireLaser, ITeleport, ILaunchMissile
    {
        protected int lastTeleportTimestamp;
        protected int teleportFrequency;
        protected int lastLaserFireTimestamp;
        protected int laserReloadFrequency;
        protected int lastMissileLaunchTimestamp;
        protected int missileReloadFrequency;
        protected int missileCount;
        protected int availableHP;

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
            absMaxDisplacement = 5;

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
            int newLocationX = LocationX + displacementX;
            if (isOutOfBoundsX(newLocationX))
            {
                displacementX *= -1;
                return;
            }
            LocationX = newLocationX;
        }

        protected override void moveVertically()
        {
            int newLocationY = LocationY + displacementY;
            if (isOutOfBoundsY(newLocationY))
            {
                displacementY *= -1;
                return;
            }
            LocationY = newLocationY;
        }

        protected bool laserIsReloading()
        {
            int laserInactivityTimeSpan = TimeManager.GameDuration - lastLaserFireTimestamp;
            return laserInactivityTimeSpan < laserReloadFrequency;
        }

        protected bool teleportClockIsReloading()
        {
            int teleportClockInactivityTimeSpan = TimeManager.GameDuration - lastTeleportTimestamp;
            return teleportClockInactivityTimeSpan < teleportFrequency;
        }

        protected bool missileIsReloading()
        {
            int missileInactivityTimeSpan = TimeManager.GameDuration - lastMissileLaunchTimestamp;
            return missileInactivityTimeSpan < missileReloadFrequency;
        }
    }
}
