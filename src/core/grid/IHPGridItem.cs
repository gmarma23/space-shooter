namespace SpaceShooter.core
{
    public interface IHPGridItem : IGridItem
    {
        public int TotalHP { get; }
        public int AvailableHP { get; }

        public float GetAvailableHealthRatio();

        public void TakeDamage(int damage);

        public void RestoreHealth(int health);
    }
}
