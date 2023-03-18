namespace SpaceShooter.core
{
    public class EnemyMissile : Missile
    {
        public EnemyMissile(ILaunchMissile missileLauncher, GameGrid grid) : base(missileLauncher, grid)
        {
            LocationY = missileLauncher.LocationY + missileLauncher.Height;
            Image = resources.Resources.img_enemy_missile;
            displacementY = absMaxDisplacement;
        }
    }
}
