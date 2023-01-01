namespace SpaceShooter.core
{
    internal interface IDrawGameStateUI
    {
        public int Score { get; }

        public (int, int) GetSpaceshipSize(bool isHero);

        public (int, int) GetSpaceshipLocation(bool isHero);

        public double GetSpaceshipAvailableHealthRatio(bool isHero);

        public EnemySpaceshipType GetEnemySpaceshipType();

        public (int, int) GetLaserBlastSize(int numCode);

        public (int, int) GetLaserBlastLocation(int numCode);

        public bool IsHeroLaserBlast(int numCode);
    }
}
