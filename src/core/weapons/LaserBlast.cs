using System.Diagnostics;

namespace SpaceShooter.core
{
    internal class LaserBlast : IGridItem
    {
        private const double laserBlastWidthRatio = 0.05;
        private const double laserBlastHeightRatio = 7;

        private static int nextNumCode = 0;
        private int displacementX;
        private int displacementY;
        private int minY;
        private int maxY;

        public bool HasHitedTarget { get; set; }
        public bool IsOutOfBounds { get; private set; }
        public int NumCode { get; private init; }
        public bool IsHero { get; private init; }
        public int Damage { get; private init; }
        public int LocationX { get; private init; }
        public int LocationY { get; private set; }
        public int Width { get; private init; }
        public int Height { get; private init; }
        private int DisplacementY
        {
            init
            {
                if (value < 0)
                    throw new ArgumentException();
                displacementY = value;
            }
        }

        public LaserBlast(Spaceship spaceship, GameGrid grid, int index)
        {
            IsHero = spaceship.IsHero;
            Damage = spaceship.LaserBlastDamage;
            displacementX = 0;
            DisplacementY = 5;
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetItemMaxPossibleY(this);

            Width = (int)(spaceship.Width * laserBlastWidthRatio);
            Height = (int)(Width * laserBlastHeightRatio);

            LocationX = spaceship.LocationX + ((index + 1) * spaceship.Width / (spaceship.ConcurrentLaserBlastsCount + 1)) - (Width / 2);
            LocationY = IsHero ? spaceship.LocationY - Height : spaceship.LocationY + spaceship.Height;

            NumCode = nextNumCode;
            nextNumCode += 1;
        }

        public void Move()
        {
            Debug.Assert(!IsOutOfBounds || !HasHitedTarget);
            LocationY += (IsHero ? -1 : 1) * displacementY;
            checkIsOutOfBounds();
        }

        public void checkIsOutOfBounds()
        {
            if (LocationY < minY || LocationY > maxY)
                IsOutOfBounds = true;
        }
    }
}
