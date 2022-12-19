namespace SpaceShooter.core
{
    internal interface IEnemySpaceship
    {
        public int ScorePoints { get; protected init; } 

        public int RenewDisplacementFrequency { get; protected set; }

        public void Teleport(int minX, int maxX, int minY, int maxY);

        public void RenewDisplacement();
    }
}
