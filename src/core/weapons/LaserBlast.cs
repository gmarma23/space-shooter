namespace SpaceShooter.core
{
    internal class LaserBlast
    {
        private const double laserBlastWidthRatio = 0.05;
        private const double laserBlastHeightRatio = 7;

        public bool IsEnemy { get; private init; }
        public int Damage { get; private init; }
        public int XLocation { get; private init; }
        public int Width { get; private init; }
        public int Height { get; private init; }
        public int YDisplacement
        {
            get => YDisplacement;
            private init
            {
                if (value < 0)
                    throw new ArgumentException();
                _ = value;
            }
        }
        public int YLocation { get; private set; }

        public LaserBlast(Spaceship spaceship, int index)
        {
            IsEnemy = spaceship.IsEnemy;
            Damage = spaceship.LaserBlastDamage;
            YDisplacement = 5;

            Width = (int)(spaceship.Width * laserBlastWidthRatio);
            Height = (int)(Width * laserBlastHeightRatio);

            XLocation = spaceship.XLocation + (index * spaceship.Width / (spaceship.ConcurrentLaserBlastsCount + 1)) - (Width / 2);
            YLocation = IsEnemy ? spaceship.YLocation + spaceship.Height : spaceship.YLocation - Height;
        } 

        public void MoveVertically()
        {
            YLocation = (IsEnemy ? 1 : -1) * YDisplacement; 
        }
    }
}
