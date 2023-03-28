namespace SpaceShooter.core
{ 
    public abstract class PowerUp : CollidableItem
    {
        public PowerUp(GameGrid grid, IGridItem source)
        {
            defaultWidthRatio = 0.05f;
            defaultHeightRatio = 1;
            absMaxDisplacement = 420;

            setSize(grid);
            setBounds(grid);

            LocationX = source.LocationX;
            LocationY = source.LocationY;

            displacementX = 0;
            displacementY = absMaxDisplacement;
        }
    }
}
