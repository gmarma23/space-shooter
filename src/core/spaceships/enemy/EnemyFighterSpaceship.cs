namespace SpaceShooter.core
{
    internal class EnemyFighterSpaceship : EnemySpaceship
    {
        public EnemyFighterSpaceship(
            int hp, int concurrentLaserBlastsCount, int laserBlastDamage, int laserReload,
            int missileCount, int missileDamage, int missileReload, bool movesRandomly, int scorePoints) : 
            base(hp, concurrentLaserBlastsCount, laserBlastDamage, laserReload, missileCount, missileDamage, missileReload, movesRandomly, scorePoints)
        {

        }

        public override void Teleport(int minX, int maxX, int minY, int maxY)
        {

        }

        public override void RenewDisplacement()
        {

        }
    }
}
