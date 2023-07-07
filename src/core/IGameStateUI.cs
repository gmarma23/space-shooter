namespace SpaceShooter.core
{
    public interface IGameStateUI
    {
        public GameGrid Grid { get; }

        public int Score { get; }

        public int Wave { get; }

        public IHPGridItem GetSpaceshipToDraw(bool isHero);

        public IGridItem? GetCollidableItemToDraw(int id);

        public List<int> GetActiveCollidableItemIDs();
    }
}
