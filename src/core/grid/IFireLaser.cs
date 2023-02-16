namespace SpaceShooter.core
{
    public interface IFireLaser : IGridItem
    {
        public int LaserBlastDamage { get; }
        public int ConcurrentLaserBlastsCount { get; }

        public List<LaserBlast>? FireLaser(GameGrid grid);
    }
}
