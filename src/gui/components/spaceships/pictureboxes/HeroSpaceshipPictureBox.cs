namespace SpaceShooter.gui
{
    internal class HeroSpaceshipPictureBox : SpaceshipPictureBox
    {
        public HeroSpaceshipPictureBox(int width, int height) : base(width, height)
        {
            Image = resources.Resources.hero_spaceship;
        }
    }
}
