namespace SpaceShooter.core
{
    internal class HeroSpaceship : Spaceship
    {
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }

        public HeroSpaceship(
            int hp, int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload,
            int missileCount, int missileDamage, int missileReload, bool movesRandomly, int scorePoints) :
            base(hp, concurrentLaserBlastsCount, laserBlastDamage, laserReload, missileCount, missileDamage, missileReload)
        {

        }
    }
}
