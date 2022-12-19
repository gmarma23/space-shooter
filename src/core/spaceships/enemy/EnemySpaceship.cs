namespace SpaceShooter.core
{
    internal abstract class EnemySpaceship : Spaceship
    {
        public int ScorePoints { get; protected init; }
        public int RenewDisplacementFrequency { get; protected set; }

        public abstract void Teleport(int minX, int maxX, int minY, int maxY);

        public abstract void RenewDisplacement();
    }
}
