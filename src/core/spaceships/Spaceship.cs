using System.Reflection.Metadata.Ecma335;

namespace SpaceShooter.core
{
    internal class Spaceship
    {
        protected const double spaceshipWidthRatio = 0.05;
        protected const double spaceshipHeightRatio = 1.05;

        public bool IsEnemy { get; protected init; }
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

        public int LaserReloadTime
        {
            get => LaserReloadTime;
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

        public Spaceship(bool isEnemy, int initXLocation, int initYLocation, int gridXDimension, int hp, 
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime, 
                         int missileCount, int missileDamage, int missileReload)
        {
            IsEnemy = isEnemy;
            XLocation = initXLocation;
            YLocation = initYLocation;
            TotalHP = hp;
            ConcurrentLaserBlastsCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            LaserReloadTime = laserReloadTime;
            MissileCount = missileCount;
            MissileDamage = missileDamage;
            MissileReload = missileReload;

            Width = (int)(gridXDimension * spaceshipWidthRatio);
            Height = (int)(Width * spaceshipHeightRatio);

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

        public List<LaserBlast> FireLaser()
        {
            List<LaserBlast> laserBlasts = new List<LaserBlast>();
            for (int i = 0; i < ConcurrentLaserBlastsCount; i++)
                laserBlasts.Add(new LaserBlast(this, i));
            return laserBlasts;
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
