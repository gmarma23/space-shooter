namespace SpaceShooter.src.core.grid
{
    public interface IControls
    {
        public bool GoUp { get; set; }
        public bool GoDown { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }

        public void ResetControls();
    }
}
