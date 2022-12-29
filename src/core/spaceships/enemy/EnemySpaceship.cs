using System;
using System.CodeDom.Compiler;

namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        protected const int updateDisplacementFrequency = 30;

        protected int moveCount;
        protected Random random;

        public EnemySpaceshipType Type { get; protected init; }
        public bool IsReadyForBattle { get; protected set; }
        public int ScorePoints { get; protected init; }

        public EnemySpaceship(EnemySpaceshipType enemyType, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime, int scorePoints) :
            base(false, 7, hp, concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime)
        {
            Type = enemyType;
            ScorePoints = scorePoints;
            IsReadyForBattle = false;
            LaserIsReloading = false;
            random = new Random();
        }

        public void BringToBattle()
        {
            if (LocationY < (minY + maxY) / 2)
                LocationY += absMaxDisplacement;
            else
                IsReadyForBattle = true;
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
            LocationX = random.Next(minX, maxX);
        }

        protected override void initializeLocationY()
        {
            LocationY = minY; //- Height;
        }

        protected int generateRandomDisplacement(bool isAbsVal = false)
        {
            return random.Next(
                isAbsVal ? 0 : -absMaxDisplacement, 
                absMaxDisplacement);
        }
    }

    public enum EnemySpaceshipType 
    { 
        Fighter,
        Teleporter,
        Boss
    }
}
