namespace SpaceShooter.core
{
    internal class LaserBlast : IGridItem
    {
        private const double laserBlastWidthRatio = 0.05;
        private const double laserBlastHeightRatio = 7;

        private static int nextNumCode = 0; 
            
        public int NumCode { get; private init; }
        public bool IsEnemy { get; private init; }
        public int Damage { get; private init; }
        public int LocationX { get; private init; }
        public int LocationY { get; private set; }
        public int Width { get; private init; }
        public int Height { get; private init; }
        public int DisplacementX { get; private init; }
        public int DisplacementY
        {
            get => DisplacementY;
            private init
            {
                if (value < 0)
                    throw new ArgumentException();
                _ = value;
            }
        }

        public LaserBlast(Spaceship spaceship, int index)
        {
            IsEnemy = spaceship.IsEnemy;
            Damage = spaceship.LaserBlastDamage;
            DisplacementX = 0;
            DisplacementY = 5;

            Width = (int)(spaceship.Width * laserBlastWidthRatio);
            Height = (int)(Width * laserBlastHeightRatio);

            LocationX = spaceship.LocationX + (index * spaceship.Width / (spaceship.ConcurrentLaserBlastsCount + 1)) - (Width / 2);
            LocationY = IsEnemy ? spaceship.LocationY + spaceship.Height : spaceship.LocationY - Height;

            NumCode = nextNumCode;
            nextNumCode += 1;
        } 

        public void MoveVertically()
        {
            LocationY = (IsEnemy ? 1 : -1) * DisplacementY; 
        }

        public void MoveHorizontally() { }
    }
}
