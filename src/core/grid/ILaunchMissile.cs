namespace SpaceShooter.core
{
    public interface ILaunchMissile : IGridItem
    {
        public int MissileDamage { get; }

        public Missile? LaunchMissile(GameGrid grid);
    }
}
