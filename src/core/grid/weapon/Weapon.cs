namespace SpaceShooter.core
{
    public abstract class Weapon : GridItem
    {
        private static int id = 0;

        protected int damage;
        
        public IHPGridItem? Target { get; set; }
        public int ID { get; protected init; }

        public Weapon() 
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
            checkTargetHit();
        }

        protected override void moveVertically()
            => LocationY += DeltaTimeDisplacementY;

        protected override void moveHorizontally()
            => LocationX += DeltaTimeDisplacementX;

        protected void checkTargetHit()
        {
            if (Target == null)
                return;

            if (!Target.IsActive)
            {
                Target = null;
                return;
            }

            if (GameGrid.ItemsIntersect(this, Target))
            {
                Target.TakeDamage(damage);
                IsActive = false;
            }
        }

        protected override void setBounds(GameGrid grid)
        {
            minX = grid.GetItemMinPossibleX();
            maxX = grid.GetItemMaxPossibleX(this);
            minY = grid.GetItemMinPossibleY();
            maxY = grid.GetItemMaxPossibleY(this);
        }
    }
}
