namespace SpaceShooter.core
{
    public abstract class PowerUp : GridItem
    {
        private static int id = 0;

        public IHPGridItem? Target { get; set; }
        public int ID { get; protected init; }

        public PowerUp()
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
            checkTargetReceived();
        }
        
        protected abstract void checkTargetReceived();

        protected override void moveVertically()
            => LocationY += DeltaTimeDisplacementY;

        protected override void moveHorizontally() { }

        protected override void setBounds(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetItemMaxPossibleY(this);
        }
    }
}
