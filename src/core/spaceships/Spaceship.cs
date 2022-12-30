using SpaceShooter.utils;

namespace SpaceShooter.core
{
    internal abstract class Spaceship : IGridItem
    {
        protected const double defaultWidthRatio = 0.065;
        protected const double defaultHeightRatio = 1.04;

        protected readonly int totalHP;

        protected int absMaxDisplacement;
        protected int displacementX;
        protected int displacementY;
        protected int concurrentLaserBlastsCount;
        protected int laserBlastDamage;
        protected int laserReloadTime;
        protected int availableHP;
        
        protected int minX;
        protected int maxX;
        protected int minY;
        protected int maxY;

        public bool IsHero { get; protected init; }
        public int LocationX { get; protected set; }
        public int LocationY { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public bool LaserIsReloading { get; set; }
        public bool IsDestroyed { get; protected set; }

        public int ConcurrentLaserBlastsCount
        {
            get => concurrentLaserBlastsCount;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
                concurrentLaserBlastsCount = value;
            }
        }

        public int LaserBlastDamage
        {
            get => laserBlastDamage;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
                laserBlastDamage = value;
            }
        }

        public int LaserReloadTime
        {
            get => laserReloadTime;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException();
                laserReloadTime = value;
            }
        }

        protected int AbsMaxDisplacement
        {
            init
            {
                if (value < 0)
                    throw new ArgumentException();
                absMaxDisplacement = value;
            }
        }

        protected int TotalHP
        {
            init
            {
                if (value < 0)
                    throw new ArgumentException();
                totalHP = value;
            }
        }

        protected int AvailableHP
        {
            set
            {
                if (value < 0)
                    availableHP = 0;
                else if (value > totalHP)
                    availableHP = totalHP;
                else
                    availableHP = value;
            }
        }

        public Spaceship(bool isHero, int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime)
        {
            IsHero = isHero;
            TotalHP = hp;
            ConcurrentLaserBlastsCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            LaserReloadTime = laserReloadTime;

            this.absMaxDisplacement = absMaxDisplacement;

            AvailableHP = totalHP;
            IsDestroyed = false;
        }

        public abstract void Move();

        public List<LaserBlast> FireLaser(GameGrid grid)
        {
            List<LaserBlast> laserBlasts = new List<LaserBlast>();
            if (LaserIsReloading) return laserBlasts;
        
            for (int i = 0; i < ConcurrentLaserBlastsCount; i++)
                laserBlasts.Add(new LaserBlast(this, grid, i));
            return laserBlasts;
        }

        public void TakeDamage(int damage) 
        {
            AvailableHP = availableHP - damage;
            if(availableHP == 0) 
                IsDestroyed = true;
        }

        public void RestoreHealth(int health)
        {
            AvailableHP = availableHP + health;
        }

        public double GetAvailableHealthRatio() 
        {
            return (double)availableHP / (double)totalHP;
        }

        protected void moveHorizontally()
        {
            int newLocationX = LocationX + displacementX;
            if (newLocationX < minX || newLocationX > maxX)
                throw new InvalidMoveException();
            LocationX += displacementX;
        }

        protected void moveVertically()
        {
            int newLocationY = LocationY + displacementY;
            if (newLocationY < minY || newLocationY > maxY)
                throw new InvalidMoveException();
            LocationY += displacementY;
        }

        protected void setSize(GameGrid grid, double scaleFactor = 1)
        {
            Width = (int)(grid.DimensionX * defaultWidthRatio * scaleFactor);
            Height = (int)(Width * defaultHeightRatio);
        }

        protected abstract void setGridLimits(GameGrid grid);

        protected abstract void initializeLocationX();

        protected abstract void initializeLocationY();
    }
}
