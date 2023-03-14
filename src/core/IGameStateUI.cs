namespace SpaceShooter.core
{
    public interface IGameStateUI
    {
        public GameGrid Grid { get; }

        public IHPGridItem GetSpaceshipToDraw(bool isHero);

        public IGridItem? GetWeaponToDraw(int id);

        public List<int> GetActiveWeaponIDs();
    }
}
