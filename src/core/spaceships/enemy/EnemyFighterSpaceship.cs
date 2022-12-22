﻿namespace SpaceShooter.core
{
    internal class EnemyFighterSpaceship : EnemySpaceship
    {
        public EnemyFighterSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int gridXDimension, int hp,
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
                         int missileCount, int missileDamage, int missileReloadTime) :
            base(defaultDisplacement, initXLocation, initYLocation, gridXDimension, hp,
                concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime,
                missileCount, missileDamage, missileReloadTime)
        {

        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
