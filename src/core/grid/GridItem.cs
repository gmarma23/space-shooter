namespace SpaceShooter.core
{
    public abstract class GridItem : IGridItem, IMove
    {
        public bool IsActive { get; protected set; }
        public int LocationX { get; protected set; }
        public int LocationY { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Image Image { get; protected set; }

        protected float defaultWidthRatio;
        protected float defaultHeightRatio;

        protected int minX;
        protected int maxX;
        protected int minY;
        protected int maxY;

        protected int absMaxDisplacement;
        protected int displacementX;
        protected int displacementY;

        protected int DeltaTimeDisplacementX
        {
            get => (int)(displacementX * TimeManager.DeltaTime);
        }

        protected int DeltaTimeDisplacementY
        {
            get => (int)(displacementY * TimeManager.DeltaTime);
        }

        protected void setSize(GameGrid grid, float scaleFactor = 1)
        {
            if (scaleFactor <= 0)
                throw new ArgumentException("Scale factor argument must have a positive value");

            Width = (int)(grid.GetDimensionAverage() * defaultWidthRatio * scaleFactor);
            Height = (int)(Width * defaultHeightRatio);
        }

        public abstract void Move();

        protected abstract void moveHorizontally();

        protected abstract void moveVertically();

        protected abstract void setBounds(GameGrid grid);

        protected bool isOutOfBoundsX(int locationX) => locationX < minX || locationX > maxX;

        protected bool isOutOfBoundsY(int locationY) => locationY < minY || locationY > maxY;

        protected bool isOutOfBoundsX() => isOutOfBoundsX(LocationX);

        protected bool isOutOfBoundsY() => isOutOfBoundsY(LocationY);
    }
}
