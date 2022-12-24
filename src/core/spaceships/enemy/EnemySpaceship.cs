using System;

namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        protected const double baselineYRatio = 0.9;
        protected Random rand;

        public int Index { get; protected init; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(int absMaxDisplacement, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime, int scorePoints) :
            base(true, hp, absMaxDisplacement, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime)
        {
            ScorePoints = scorePoints;
            rand = new Random();
        }

        protected void updateDisplacement(int targetX, int targetY)
        {
            DisplacementX = absMaxDisplacement;
            int dx = targetX - LocationX;
            if (Math.Sign(dx) != Math.Sign(DisplacementX))
            {
                DisplacementX *= -1;
                if (dx < DisplacementX)
                    DisplacementX = dx;
            }

            DisplacementY = absMaxDisplacement;
            int dy = targetY - LocationY;
            if (Math.Sign(dy) != Math.Sign(DisplacementY))
            {
                DisplacementY *= -1;
                if (dy < DisplacementY)
                    DisplacementY = dy;
            }
        }

        protected override void setGridLimits(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetGridMiddleY() - Height;
        }

        protected override void initializeLocationX()
        {
            LocationX = new Random().Next(minX, maxX);
        }

        protected override void initializeLocationY()
        {
            LocationY = (minY + maxY) / 2 - Height / 2;
        }

        protected int generateRandomX()
        {
            return rand.Next(minX, maxX);
        }

        protected int generateRandomY()
        {
            return rand.Next(minY, maxY);
        }
    }

    public enum EnemySpaceshipType 
    { 
        Fighter,
        Teleporter,
        Boss
    }
}
