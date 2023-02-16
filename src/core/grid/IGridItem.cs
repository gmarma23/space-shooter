namespace SpaceShooter.core
{
    public interface IGridItem
    {
        public bool IsActive { get; }
        public int LocationX { get; }
        public int LocationY { get; }
        public int Width { get; }
        public int Height { get; }
        public Image Image { get; }
    }
}
