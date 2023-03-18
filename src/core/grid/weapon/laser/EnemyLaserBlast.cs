using SpaceShooter.resources;

namespace SpaceShooter.core
{
    public class EnemyLaserBlast : LaserBlast
    {
        public EnemyLaserBlast(IFireLaser laserCarrier, GameGrid grid, int index) : base(laserCarrier, grid, index)
        {
            LocationY = laserCarrier.LocationY + laserCarrier.Height;
            Image = Resources.img_red_laser_blast;
            displacementY = absMaxDisplacement;
        }
    }
}
