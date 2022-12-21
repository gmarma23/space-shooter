namespace SpaceShooter.core
{
    internal class Spaceship
    {
        public int XLocation { get; protected set; }
        public int YLocation { get; protected set; }
        public int XDisplacement { get; protected set; }
        public int YDisplacement { get; protected set; }
        public int XVelocity { get; protected set; }
        public int YVelocity { get; protected set; }
        public bool IsDestroyed { get; protected set; }

        public int TotalHP 
        { 
            get => TotalHP; 
            protected init 
            { 
                if (value < 0) 
                    throw new ArgumentException(); 
            } 
        }

        public int AvailableHP
        {
            get => AvailableHP;
            protected set
            {
                if (value < 0)
                    _ = 0;
                else if (value > TotalHP)
                    _ = TotalHP;
            }
        }

        public int ConcurrentLaserBlastCount
        {
            get => ConcurrentLaserBlastCount;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public int LaserBlastDamage
        {
            get => LaserBlastDamage;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public int LaserReload
        {
            get => LaserReload;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

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

        public int MissileReload
        {
            get => MissileReload;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        public Spaceship(
            int hp, int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload, 
            int missileCount, int missileDamage, int missileReload)
        {
            TotalHP = hp;
            ConcurrentLaserBlastCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            LaserReload = laserReload;
            MissileCount = missileCount;
            MissileDamage = missileDamage;
            MissileReload = missileReload;  

            AvailableHP = TotalHP;
            IsDestroyed = false;
        }

        public void TakeDamage(int damage) 
        {
            AvailableHP -= damage;
            if(AvailableHP == 0) 
                IsDestroyed = true;
        }

        public void RestoreHealth(int health)
        {
            AvailableHP += health;
        }
    }
}
