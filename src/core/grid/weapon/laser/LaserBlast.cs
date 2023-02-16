namespace SpaceShooter.core
{
    public abstract class LaserBlast : Weapon
    {
        public LaserBlast(IFireLaser laserCarrier, GameGrid grid, int index)
        {
            defaultWidthRatio = 0.005f;
            defaultHeightRatio = 7;
            absMaxDisplacement = 7;

            setSize(grid);
            setBounds(grid);

            LocationX = laserCarrier.LocationX + ((index + 1) * laserCarrier.Width / (laserCarrier.ConcurrentLaserBlastsCount + 1)) - (Width / 2);
            damage = laserCarrier.LaserBlastDamage;
            displacementX = 0;
        }
    }
}
