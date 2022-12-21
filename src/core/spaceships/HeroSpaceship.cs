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

        public HeroSpaceship(int defaultDisplacement, int initXLocation, int initYLocation, int width, int height, int hp,
            int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload,
            int missileCount, int missileDamage, int missileReload) :
            base (false, initXLocation, initYLocation, width, height, hp, 
                concurrentLaserBlastsCount, laserBlastDamage, laserReload, 
                missileCount, missileDamage, missileReload)
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
