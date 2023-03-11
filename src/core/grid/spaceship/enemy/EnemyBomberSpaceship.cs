namespace SpaceShooter.core
{
    public class EnemyBomberSpaceship : EnemySpaceship
    {
        public EnemyBomberSpaceship(
            GameGrid grid,
            int hp = 400,
            int concurrentLaserBlastsCount = 1,
            int laserBlastDamage = 30,
            int laserReloadFrequency = 1500,
            int missileCount = 5,
            int missileDamage = 100,
            int missileReloadFrequency = 10000,
            int scorePoints = 15
        ) : base(
            hp,
            concurrentLaserBlastsCount,
            laserBlastDamage,
            laserReloadFrequency,
            0,
            missileCount,
            missileDamage,
            missileReloadFrequency,
            scorePoints
        )
        {
            setSize(grid);
            setBounds(grid);
            initializeLocationX();
            initializeLocationY();
            Image = resources.Resources.enemy_bomber_spaceship;
        }
    }
}
