using SpaceShooter.resources;

namespace SpaceShooter.core
{
    public class HeroLaserBlast : LaserBlast
    {
        public HeroLaserBlast(IFireLaser laserCarrier, GameGrid grid, int index) : base(laserCarrier, grid, index)
        {
            LocationY = laserCarrier.LocationY - Height; 
            Image = Resources.img_blue_laser_blast;
            displacementY = -absMaxDisplacement;
        }
    }
}
