namespace SpaceShooter.core
{ 
    public abstract class PowerUp : CollidableItem
    {
        public PowerUp(GameGrid grid, IGridItem source)
        {
            defaultWidthRatio = 0.03f;
            defaultHeightRatio = 1;
            absMaxDisplacement = (int)(0.46 * grid.DimensionY); 

            setSize(grid);
            setBounds(grid);

            LocationX = source.LocationX;
            LocationY = source.LocationY;

            displacementX = 0;
            displacementY = absMaxDisplacement;
        }
    }
}
