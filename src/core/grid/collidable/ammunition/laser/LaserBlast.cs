namespace SpaceShooter.core
{
    public abstract class LaserBlast : Ammunition
    {
        public LaserBlast(IFireLaser laserCarrier, GameGrid grid, int index)
        {
            defaultWidthRatio = 0.005f;
            defaultHeightRatio = 7;
            absMaxDisplacement = (int)(0.553 * grid.DimensionY);

            setSize(grid);
            setBounds(grid);

            LocationX = laserCarrier.LocationX + ((index + 1) * laserCarrier.Width / (laserCarrier.ConcurrentLaserBlastsCount + 1)) - (Width / 2);
            damage = laserCarrier.LaserBlastDamage;
            displacementX = 0;
        }
    }
}
