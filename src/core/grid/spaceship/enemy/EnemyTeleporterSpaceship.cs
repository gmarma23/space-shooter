namespace SpaceShooter.core
{
    public class EnemyTeleporterSpaceship : EnemySpaceship
    {
        public EnemyTeleporterSpaceship(
            GameGrid grid, 
            int hp = 400,
            int concurrentLaserBlastsCount = 1, 
            int laserBlastDamage = 30, 
            int laserReloadFrequency = 1500, 
            int scorePoints = 10
        ) : base (hp, concurrentLaserBlastsCount, laserBlastDamage, laserReloadFrequency, 0, 0, 0, scorePoints)
        {
            setSize(grid);
            setBounds(grid);
            initializeLocationX();
            initializeLocationY();
            Image = resources.Resources.enemy_teleporter_spaceship;
        }

        public override void Teleport()
        {
            LocationX = random.Next(minX, maxX);
            LocationY = random.Next(minY, maxY);
        }
    }
}
