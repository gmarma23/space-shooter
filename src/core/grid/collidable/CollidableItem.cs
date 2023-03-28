 namespace SpaceShooter.core
{
    public abstract class CollidableItem : GridItem
    {
        private static int id = 0;

        protected int damage;

        public IHPGridItem? Target { get; set; }
        public int ID { get; protected init; }

        public CollidableItem()
        {
            Target = null;
            IsActive = true;
            ID = id++;
        }

        public override void Move()
        {
            if (!IsActive)
                return;

            moveVertically();
            moveHorizontally();
            checkTargetCollided();
        }

        protected abstract void checkTargetCollided();

        protected override void moveVertically()
            => LocationY += DeltaTimeDisplacementY;

        protected override void moveHorizontally()
            => LocationX += DeltaTimeDisplacementX;

        protected override void setBounds(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetItemMaxPossibleY(this);
        }
    }
}
