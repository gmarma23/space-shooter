namespace SpaceShooter.core
{
    internal class LaserBlast
    {
        public bool IsEnemy { get; private init; }
        public int Damage { get; private init; }
        public int XLocation { get; private init; }
        public int Width { get; private init; }
        public int Height { get; private init; }
        public int YDisplacement
        {
            get => YDisplacement;
            private init
            {
                if (value < 0)
                    throw new ArgumentException();
                _ = value;
            }
        }
        public int YLocation { get; private set; }

        public LaserBlast(bool isEnemy, int damage, int displacement, int initXLocation, int initYLocation, int width, int height)
        {
            IsEnemy = isEnemy;
            Damage = damage;
            YDisplacement = displacement;
            XLocation = initXLocation;
            YLocation = initYLocation;
            Width = width;
            Height = height;
        } 

        public void MoveVertically()
        {
            YLocation = (IsEnemy ? 1 : -1) * YDisplacement; 
        }
    }
}
