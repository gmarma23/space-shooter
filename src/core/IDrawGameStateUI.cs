namespace SpaceShooter.core
{
    internal interface IDrawGameStateUI
    {
        public (int, int) GetSpaceshipSize(bool isHero);

        public (int, int) GetSpaceshipLocation(bool isHero);

        public EnemySpaceshipType GetEnemySpaceshipType();

        public (int, int) GetLaserBlastSize(int numCode);

        public (int, int) GetLaserBlastLocation(int numCode);

        public bool IsHeroLaserBlast(int numCode);
    }
}
