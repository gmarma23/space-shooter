using System.Diagnostics;

namespace SpaceShooter.core
{
    internal class HeroSpaceship : Spaceship
    {
        private readonly int defaultDisplacement;

        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }

        public HeroSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int gridXDimension, int hp,
                         int concurrentLaserBlastsCount, int laserBlastDamage, int laserReloadTime,
                         int missileCount, int missileDamage, int missileReloadTime) :
            base (false, initXLocation, initYLocation, gridXDimension, hp, 
                concurrentLaserBlastsCount, laserBlastDamage, laserReloadTime, 
                missileCount, missileDamage, missileReloadTime)
        {
            this.defaultDisplacement = defaultDisplacement;

            GoLeft = false;
            GoRight = false;
            GoUp = false;
            GoDown = false;

            UpdateDisplacement();
        }

        public void UpdateDisplacement()
        {
            XDisplacement = 0;
            YDisplacement = 0;

            if (GoLeft) XDisplacement -= defaultDisplacement;
            if (GoRight) XDisplacement += defaultDisplacement;
            if (GoUp) YDisplacement -= defaultDisplacement;
            if (GoDown) YDisplacement += defaultDisplacement;
        }
    }
}
