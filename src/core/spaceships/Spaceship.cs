namespace SpaceShooter.core
{
    internal abstract class Spaceship : IGridItem
    {
        protected const double defaultWidthRatio = 0.05;
        protected const double defaultHeightRatio = 1.05;

        protected readonly int absMaxDisplacement;

        protected int minX;
        protected int maxX;
        protected int minY;
        protected int maxY;

        public bool IsEnemy { get; protected init; }
        public int LocationX { get; protected set; }
        public int LocationY { get; protected set; }
        public int DisplacementX { get; protected set; }
        public int DisplacementY { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public bool LaserIsReloading { get; set; }
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

        public Spaceship(bool isEnemy, int hp, int absMaxDisplacement,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime, 
            int missileCount, int missileDamage, int missileReloadTime)
        {
            IsEnemy = isEnemy;
            TotalHP = hp;
            ConcurrentLaserBlastsCount = concurrentLaserBlastsCount;
            LaserBlastDamage = laserBlastDamage;
            LaserReloadTime = laserReloadTime;

            this.absMaxDisplacement = absMaxDisplacement;

            AvailableHP = TotalHP;
            IsDestroyed = false;
        }

        public void MoveHorizontally()
        {
            int newLocationX = LocationX + DisplacementX;
            if (newLocationX < minX && newLocationX > maxX)
                throw new Exception();
            LocationX += DisplacementX;
        }

        public void MoveVertically()
        {
            int newLocationY = LocationY + DisplacementY;
            if (newLocationY < minY && newLocationY > maxY)
                throw new Exception();
            LocationY += DisplacementY;
        }

        public List<LaserBlast> FireLaser()
        {
            List<LaserBlast> laserBlasts = new List<LaserBlast>();
            if (LaserIsReloading) return laserBlasts;
        
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
