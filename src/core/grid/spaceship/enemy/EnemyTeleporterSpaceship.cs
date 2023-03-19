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
            int teleportFrequency = 5000,
            int scorePoints = 10
        ) : base(
            hp, 
            concurrentLaserBlastsCount, 
            laserBlastDamage, 
            laserReloadFrequency, 
            teleportFrequency, 
            0, 
            0, 
            0, 
            scorePoints
        )
        {
            setSize(grid);
            setBounds(grid);
            initializeLocationX();
            initializeLocationY();
            Image = resources.Resources.img_enemy_teleporter_spaceship;
        }
    }
}
