namespace SpaceShooter.core
{
    internal class Spaceship
    {
        public int XLocation { get; protected set; }
        public int YLocation { get; protected set; }
        public int XDisplacement { get; protected set; }
        public int YDisplacement { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public bool IsDestroyed { get; protected set; }

        public int ConcurrentLaserBlastsCount
        {
            get => ConcurrentLaserBlastsCount;
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

        protected int TotalHP
        {
            get => TotalHP;
            init
            {
                if (value < 0)
                    throw new ArgumentException();
            }
        }

        protected int AvailableHP
        {
            get => AvailableHP;
            set
            {
                if (value < 0)
                    _ = 0;
                else if (value > TotalHP)
                    _ = TotalHP;
            }
        }

        public Spaceship(int initXLocation, int initYLocation, int width, int height, int hp, 
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload, 
                         int missileCount, int missileDamage, int missileReload)
        {
            XLocation = initXLocation;
            YLocation = initYLocation;
            Width = width; 
            Height = height;
            TotalHP = hp;
            ConcurrentLaserBlastsCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            LaserReload = laserReload;
            MissileCount = missileCount;
            MissileDamage = missileDamage;
            MissileReload = missileReload;

            AvailableHP = TotalHP;
            IsDestroyed = false;
        }

        public void MoveHorizontally()
        {
            XLocation += XDisplacement;
        }

        public void MoveVertically()
        {
            YLocation += YDisplacement;
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

        public double GetAvailableHealthRatio() 
        {
            return AvailableHP / TotalHP;
        }
    }
}
